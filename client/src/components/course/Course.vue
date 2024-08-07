<script setup lang="ts">
import { days, getTheWeeknd, getWeekStart, hours } from '@/lib/utils';
import { ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import SideBar from '../SideBar.vue';
import SlotGroupInfo from './SlotGroupInfo.vue';
import axios from 'axios';

type SlotData = {
    isSelected: true,
    id: string,
    users: string[] // all user id's available at this slot
} | {
    isSelected: false
}

const route = useRoute()
const schedules = ref<Schedule[]>();
const isEditable = ref<boolean>(false);
const week = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
const slots = ref<Map<string, { start: Date, data: SlotData }[]>>(getSlots())
const selectedSlot = ref<{ start: Date, data: SlotData }>({ start: new Date(), data: { isSelected: false } });

function getSlots(): Map<string, { start: Date, data: SlotData }[]> {
    const start = new Date(getWeekStart().getTime() + (8 * hours));
    const slots = new Map<string, { start: Date, data: SlotData }[]>();

    for (let i = 0; i < 7; i++) {
        const dayStart = start.getTime() + (i * days);
        for (let j = dayStart; j < dayStart + (12 * hours); j += (1 * hours)) {
            if (!slots.has(week[i])) {
                slots.set(week[i], []);
            }
            const date = new Date(j);
            slots.get(week[i])?.push({ start: date, data: getIsSelected(date) });
        }
    }

    return slots;
}

function getIsSelected(date: Date): SlotData {
    const schedule = schedules.value?.find(i => i.timeFrom <= date.getTime() && i.timeTo > date.getTime());
    if (schedule && schedule.id) {
        const users = schedules.value?.filter(i =>
            i.timeFrom <= date.getTime() && i.timeTo > date.getTime()
        ).map(schedule => schedule.userId);
        if (users) {
            return { isSelected: true, id: schedule.id, users }
        }
    }
    return { isSelected: false }
}

// async function createSlot(date: Date, data: SlotData) {
//     if (!data.isSelected) {
//         await axios.post("api/schedule", {
//             schedule: [{
//                 timeFrom: date.getTime(),
//                 timeTo: date.getTime() + (1 * hours)
//             }],
//             courseId: route.params.id
//         });
//     } else {
//         await server.delete(`api/schedule/delete/${data.id}`);
//     }
// }

async function revalidate(courseId: string) {
    if (isEditable.value) {
        schedules.value = await (await axios.get(`api/schedule/week/user/course/${courseId}/${getWeekStart().getTime()}/${getTheWeeknd().getTime()}`)).data;
    } else {
        schedules.value = await (await axios.get(`api/schedule/week/course/${route.params.id}/${getWeekStart().getTime()}/${getTheWeeknd().getTime()}`)).data;
    }
    slots.value = getSlots();
}

async function handleClick(slot: Date, data: SlotData) {
    console.log(data);
    // if (isEditable.value) {
    //     await createSlot(slot, data);
    // } else {
    // handle creating chat
    if (data.isSelected) {
        console.log(data.users);
    }
    selectedSlot.value = {
        start: slot,
        data: data
    };
    // }

    await revalidate(route.params.id as string);
}

// async function handleEditable() {
//     isEditable.value = !isEditable.value;
//     await revalidate(route.params.id as string);
// }

watch(slots, (newSlots) => {
    for (const [_, slotArray] of newSlots.entries()) {
        // Find the slot with the matching start date
        if (selectedSlot.value.data.isSelected) {
            const foundSlot = slotArray.find(slot => slot.start.getTime() === selectedSlot.value.start.getTime());
            if (foundSlot) {
                selectedSlot.value = foundSlot;
            }
        }
    }
})

watch(
    () => route.params.id,
    async (id) => {
        try {
            // TODO: change url here depending on what view we are in 
            await revalidate(id as string);
            console.log(schedules.value?.map(i => i.userId));
            slots.value = getSlots();

        } catch (e) {
            console.error(e);
        }
    },
    {
        immediate: true
    }
)
// function getHeading() {
//     if (isEditable.value) {
//         return "Currently viewing your availability"
//     } else {
//         return "Currently viewing all availabilities"
//     }
// }

</script>
<template>
    <div class="flex">
        <SideBar />
        <div class="w-full align-middle p-10">
            <h4 class="mx-auto flex justify-center border-b-2 pb-3 font-bold">{{ route.params.id }}</h4>
            <!-- <div class="flex justify-between  mx-auto pt-3">
                <!-- <h4>
                    <h4 class="text-center inline mr-5">View your availability</h4>
                    <input type="checkbox" class="inline" name="editable" id="editable"
                        @change="() => handleEditable()">
                </h4> -->
            <!-- <h4>
                    {{ getHeading() }}
                </h4>
            </div> -->
            <div class="flex flex-row gap-5">
                <div class="parent flex-2">
                    <div v-for="slot in slots" class="p-2 text-center">
                        <h5>{{ slot[0] }}</h5>
                        <div v-for="{ start, data } in slot[1]" style="display: flex; flex-direction: column;"
                            class="p-2 cursor-pointer" @click="() => { handleClick(start, data) }"
                            :class="[data.isSelected ? 'bg-primary' : 'time-slot']">
                            <h6 @click="() => console.log(start, start.getTime())">{{ start.toLocaleString(undefined, {
                                hour: "2-digit",
                                minute: "2-digit",
                                second: "2-digit"
                            }) }}</h6>
                        </div>
                    </div>
                </div>
                <SlotGroupInfo class="flex-1" :revalidate="revalidate" :slotData="selectedSlot"></SlotGroupInfo>
            </div>
        </div>
    </div>
</template>

<style>
.parent {
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    grid-column-gap: 0px;
    grid-row-gap: 0px;
    margin-top: 30px;
    width: 80%;
}

.time-slot {
    background-color: whitesmoke;
}

.time-slot:hover {
    background-color: gray;
}

.bg-primary:hover {
    background-color: darkblue;
    color: white;
}
</style>