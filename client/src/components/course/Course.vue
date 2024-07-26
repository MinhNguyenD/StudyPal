<script setup lang="ts">
import { server } from '@/instance';
import { days, getWeekStart, hours } from '@/lib/utils';
import { ref, watch } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const schedules = ref<Schedule[]>();
const userId = "99cecee5-36a9-4da8-ac9c-639463b36c4b"
const week = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

function getSlots(): Map<string, { start: Date, data: { isSelected: boolean, id: string | null } }[]> {
    const start = new Date(getWeekStart().getTime() + (8 * hours));
    const slots = new Map<string, { start: Date, data: { isSelected: boolean, id: string | null } }[]>();

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

function getIsSelected(date: Date): { isSelected: boolean, id: string | null } {
    const schedule = schedules.value?.find(i => i.timeFrom <= date.getTime() && i.timeTo > date.getTime());
    if (schedule) {
        return { isSelected: true, id: schedule.id }
    }
    return { isSelected: false, id: null }
}

async function createSlot(slot: Date) {
    server.post("schedule", {
        schedule: [{
            timeFrom: slot.getTime(),
            timeTo: slot.getTime() + (1 * hours)
        }],
        userId: userId,
        courseId: route.params.id
    });

    schedules.value = await (await server.get(`schedule/week/${userId}/course/${route.params.id}`)).data;
}

const slots = getSlots();

watch(
    () => route.params.id,
    async (id) => {
        try {
            schedules.value = await (await server.get(`schedule/week/${userId}/course/${id}`)).data
            console.log(schedules.value);
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
    <h1>hello{{ route.params.id }}</h1>
    <div style="display: flex; justify-content: space-between;">
        <div class="parent">
            <div v-for="slot in slots">
                <div v-for="{ start, } in slot[1]" style="display: flex; flex-direction: column;"
                    @click="() => createSlot(start)">
                    <p @click="() => console.log(start, start.getTime())"
                        :class="[getIsSelected(start).isSelected ? 'active' : 'time-slot']">{{ start.toLocaleString(undefined, {
                            day: "2-digit",
                            hour: "2-digit",
                            minute: "2-digit",
                            second: "2-digit"
                        }) }}</p>
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
    width: 80%;
}

.active
{
    background-color: green;
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