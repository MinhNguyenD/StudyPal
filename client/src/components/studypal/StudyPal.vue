<template>
    <div class="flex">
        <SideBar class="" />
        <div class="w-full align-middle p-10">
            <div>
                <label for="role-select">Role:</label>
                <select id="role-select" v-model="role">
                    <option value="tutor">Tutor</option>
                    <option value="student">Student</option>
                </select>
            </div>
            <div class="flex flex-row gap-5">
                <div class="parent flex-2">
                    <div v-for="slot in slots" class="p-2 text-center">
                        <h5>{{ slot[0] }}</h5>
                        <div v-for="{ start, data } in slot[1]" :key="start.getTime()"
                            style="display: flex; flex-direction: column;" class="p-2 cursor-pointer"
                            @click="() => handleClick(start, data)"
                            :class="[data.isSelected ? 'bg-primary' : 'time-slot']">
                            <h6>{{ start.toLocaleString(undefined, {
                                hour: '2-digit', minute: '2-digit', second: '2-digit'
                            }) }}</h6>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="py-10 space-y-2" v-if="selectedSlot?.data.isSelected && role === 'tutor'">
                        <p class="text-3xl font-semibold pb-2 border-b">Study Session:</p>
                        <p class="text-xl pt-1">{{ }}</p>
                        <p class="text-xl">{{ formatDate(selectedSlot?.start) }}</p>
                        <p class="text-xl">{{ formatTime(selectedSlot?.start, new Date(selectedSlot?.start.getTime() +
                            60 * 60 * 1000)) }}</p>
                        <br>
                        <button @click="cancelSession"
                            class="mx-2 bg-red-500 text-white py-2 px-4 rounded-full p-2">Cancel this Session</button>
                    </div>
                    <div class="py-10 space-y-2" v-if="!selectedSlot?.data.isSelected && role === 'tutor'">
                        <p class="text-3xl font-semibold pb-2 border-b">Study Session:</p>
                        <p class="text-xl pt-1">{{ }}</p>
                        <p class="text-xl">{{ formatDate(selectedSlot?.start) }}</p>
                        <p class="text-xl">{{ formatTime(selectedSlot?.start, new Date(selectedSlot?.start.getTime() +
                            60 * 60 * 1000)) }}</p>
                        <br>
                        <button @click="createSession"
                            class="mx-2 bg-green-500 text-white py-2 px-4 rounded-full p-2">Create a Session</button>
                    </div>
                    <div class="py-10 space-y-2" v-if="selectedSlot?.data.isSelected && role === 'student'">
                        <p class="text-3xl font-semibold pb-2 border-b">Study Session:</p>
                        <p class="text-xl pt-1">{{ }}</p>
                        <p class="text-xl">{{ formatDate(selectedSlot?.start) }}</p>
                        <p class="text-xl">{{ formatTime(selectedSlot?.start, new Date(selectedSlot?.start.getTime() +
                            60 * 60 * 1000)) }}</p>
                        <br>
                        <button @click="joinSession" class="mx-2 bg-primary text-white py-2 px-4 rounded-full p-2">Join
                            this Session</button>
                        <button @click="leaveSession"
                            class="mx-2 bg-red-500 text-white py-2 px-4 rounded-full p-2">Leave this Session</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { useUserStore } from "@/store/user";
import SideBar from "../SideBar.vue";
import { ref, watch, onMounted } from "vue";
import { days, getWeekStart, hours } from '@/lib/utils';
import axios from "axios";
import { TutorSchedule } from "@/models/Schedule";

interface SlotDataSelected {
    isSelected: true;
    id: string;
    users: string[];
    eventId: string;
}

interface SlotDataUnselected {
    isSelected: false;
    eventId: string;
}

interface Slot {
    start: Date;
    data: SlotData;
}

type SlotData = SlotDataSelected | SlotDataUnselected;

export default {
    components: {
        SideBar,
    },
    setup() {
        const role = ref<'tutor' | 'student'>('student');
        const userStore = useUserStore();
        const tutorSchedules = ref<TutorSchedule[]>([]);
        const slots = ref(new Map<string, Slot[]>());
        const week = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        const userName = userStore.storedUser?.username || '';
        const selectedSlot = ref<Slot>({
            start: new Date(),
            data: { isSelected: false, eventId: "" }
        });

        const formatDate = (date: Date) => {
            const options: Intl.DateTimeFormatOptions = { month: 'long', day: 'numeric', year: 'numeric' };
            return date.toLocaleDateString('en-US', options);
        }

        const formatTime = (start: Date, end: Date) => {
            const options: Intl.DateTimeFormatOptions = { hour: 'numeric', minute: 'numeric' };
            const startTime = start.toLocaleTimeString('en-US', options);
            const endTime = end.toLocaleTimeString('en-US', options);
            return `${startTime} - ${endTime}`;
        }

        const onViewChanged = (newRole: 'tutor' | 'student') => {
            fetchTutorSchedules();

            if (userName) {

                if (newRole === "student") {
                    tutorSchedules.value = tutorSchedules.value.filter(i => i.userId !== userName && i.users.includes(userName));
                } else if (newRole === "tutor") {
                    tutorSchedules.value = tutorSchedules.value.filter(i => i.userId === userName);
                }
            }

            slots.value = getSlots();
        };

        const fetchTutorSchedules = async () => {
            try {
                const response = await axios.get(`/api/tutorschedule`);
                tutorSchedules.value = response.data.map((schedule: any) => ({
                    userId: schedule.username,
                    timeFrom: schedule.timeFrom,
                    timeTo: schedule.timeTo,
                    courseId: schedule.courseId,
                    eventId: schedule.eventId,
                    users: schedule.users
                }));
            } catch (error) {
                console.error(error);
            }
        };

        const getSlots = (): Map<string, Slot[]> => {
            const start = new Date(getWeekStart().getTime() + 8 * hours);
            const slotsMap = new Map<string, Slot[]>();

            for (let i = 0; i < 7; i++) {
                const dayStart = start.getTime() + i * days;
                for (let j = dayStart; j < dayStart + 12 * hours; j += 1 * hours) {
                    const date = new Date(j);
                    const slotData = getIsSelected(date);
                    if (!slotsMap.has(week[i])) {
                        slotsMap.set(week[i], []);
                    }

                    const slot: Slot = {
                        start: date,
                        data: slotData,
                    };
                    slotsMap.get(week[i])?.push(slot);
                }
            }

            return slotsMap;
        };

        const fillSlots = () => {
            fetchTutorSchedules();

            if (userName) {
                tutorSchedules.value = tutorSchedules.value.filter(i => i.userId !== userName && i.users.includes(userName));
            }
            slots.value = getSlots();
        }

        function createEventId(courseId: string, date: Date): string {
            const startTime = date.getTime();
            const endTime = startTime + (60 * 60 * 1000);
            return `${courseId}-${userName}-${startTime}-${endTime}`;
        }

        const getIsSelected = (date: Date): SlotData => {
            const schedule = tutorSchedules.value.find((i: TutorSchedule) => i.timeFrom === date.getTime());
            if (schedule) {
                const users = tutorSchedules.value
                    ?.filter(i => i.timeFrom <= date.getTime() && i.timeTo > date.getTime())
                    .map(schedule => schedule.userId);
                if (users) {
                    return { isSelected: true, id: schedule.userId, users, eventId: "" };
                }
            }
            return { isSelected: false, eventId: "" };
        }

        const handleClick = async (slot: Date, data: SlotData) => {
            console.log(data)
            selectedSlot.value = { start: slot, data: data };
            await fetchTutorSchedules();
        };

        const leaveSession = async () => {
            try {
                const eventId = createEventId("", selectedSlot.value.start);
                await axios.put(`api/tutorSchedules/addUser/${eventId}/${userName}`);
            } catch (error) {
                console.log(error);
            }
        }

        const joinSession = async () => {
            try {
                const eventId = createEventId("", selectedSlot.value.start);
                await axios.put(`api/tutorSchedules/removeUser/${eventId}/${userName}`);
            } catch (error) {
                console.log(error);
            }
        }

        const createSession = async () => {
            try {
                await axios.put(`api/tutorSchedules`, {
                    username: userName,
                    eventId: createEventId("", selectedSlot.value.start),
                    timeFrom: selectedSlot.value.start.getTime(),
                    timeTo: selectedSlot.value.start.getTime() + (60 * 60 * 1000),
                    courseId: "",
                    users: []
                });
            } catch (error) {
                console.log(error);
            }
        }

        const cancelSession = async () => {
            try {
                const eventId = createEventId("", selectedSlot.value.start);
                await axios.put(`api/tutorSchedules/delete/${eventId}`);
            } catch (error) {
                console.log(error);
            }
        }

        watch(role, async (newRole) => {
            await onViewChanged(newRole);
        });

        onMounted(async () => {
            await onViewChanged(role.value);
        });

        return {
            role,
            slots,
            selectedSlot,
            handleClick,
            onViewChanged,
            fetchTutorSchedules,
            fillSlots,
            formatDate,
            formatTime,
            leaveSession,
            joinSession,
            cancelSession,
            createSession
        };
    },
    async mounted() {
        this.role = "student";
        this.fillSlots();
    }
};
</script>

<style scoped>
.parent {
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    grid-column-gap: 0px;
    grid-row-gap: 0px;
}

.time-slot {
    background-color: whitesmoke;
}

.time-slot:hover {
    background-color: gray;
    cursor: pointer;
}
</style>
