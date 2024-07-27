<script setup lang="ts">
import { server } from '@/instance';
import { days, getWeekStart, hours } from '@/lib/utils';
import { useUserStore } from '@/store/user';
import { jwtDecode } from 'jwt-decode';
import { ref, watch } from 'vue'
import { useRoute } from 'vue-router'

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

function getUserId() {
    const user = useUserStore();
    if (user.storedUser) {
        const id = jwtDecode(user.storedUser.token);

        return id.sub;
    }

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

async function createSlot(date: Date, data: SlotData) {
    if (!data.isSelected) {
        await server.post("api/schedule", {
            schedule: [{
                timeFrom: date.getTime(),
                timeTo: date.getTime() + (1 * hours)
            }],
            courseId: route.params.id
        });
    } else {
        await server.delete(`api/schedule/delete/${data.id}`);
    }
}

async function revalidate(courseId: string) {
    const currentUser = getUserId();
    if (isEditable.value) {
        schedules.value = await (await server.get(`api/schedule/week/user/${currentUser}/course/${courseId}`)).data;
        // schedules.value = schedules.value?.filter(schedule => schedule.userId === currentUser)
    } else {
        schedules.value = await (await server.get(`api/schedule/week/course/${route.params.id}`)).data;
    }
    slots.value = getSlots();
}

async function handleClick(slot: Date, data: SlotData) {
    console.log(data);
    if (isEditable.value) {
        await createSlot(slot, data);
    } else {
        // handle getting user id's
        if (data.isSelected) {
            console.log(data.users);
        }
    }

    await revalidate(route.params.id as string);
}

async function handleEditable() {
    isEditable.value = !isEditable.value;
    await revalidate(route.params.id as string);
}

watch(
    () => route.params.id,
    async (id) => {
        try {
            // TODO: change url here depending on what view we are in 

            await revalidate(id as string);
            // if (response.status === 404) {
            //     alert("Course Not Found");
            // }
            // schedules.value = await response.data;
            console.log(getUserId());
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
</script>
<template>
    <div style="display: flex; justify-content: space-between;">
        <input type="checkbox" name="editable" id="editable" @change="() => handleEditable()">
        <div class="parent">
            <div v-for="slot in slots">
                <h5>{{ slot[0] }}</h5>
                <div v-for="{ start, data } in slot[1]" style="display: flex; flex-direction: column;"
                    @click="() => handleClick(start, data)">
                    <h6 @click="() => console.log(start, start.getTime())"
                        :class="[data.isSelected ? 'bg-primary' : 'time-slot']">{{ start.toLocaleString(undefined, {
                            hour: "2-digit",
                            minute: "2-digit",
                            second: "2-digit"
                        }) }}</h6>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
.parent
{
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    grid-column-gap: 0px;
    grid-row-gap: 0px;
    margin: auto;
    margin-top: 60px;
    width: 80%;
}

.time-slot
{
    background-color: whitesmoke;
}

.time-slot:hover
{
    background-color: gray;
}
</style>