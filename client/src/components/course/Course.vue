<script setup lang="ts">
import { days, getWeekStart, hours } from '@/lib/utils';
import { ref, watch } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const schedules = ref<Schedule[]>();
const userId = "99cecee5-36a9-4da8-ac9c-639463b36c4b"
const week = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

function getSlots(): Map<string, Date[]> {
    const start = new Date(getWeekStart().getTime() + (8 * hours));
    const slots = new Map<string, Date[]>();

    for (let i = 0; i < 7; i++) {
        const dayStart = start.getTime() + (i * days);
        for (let j = dayStart; j < dayStart + (12 * hours); j += (1 * hours)) {
            if (!slots.has(week[i])) {
                slots.set(week[i], []);
            }
            slots.get(week[i])?.push(new Date(j));
        }
    }

    return slots;
}

function isSelected(date: Date): boolean {
    const val = schedules.value?.some(i => i.timeFrom < date.getTime() && i.timeTo > date.getTime() ) ?? false
    console.log(val);
    return val;
}

const slots = getSlots();

watch(
    () => route.params.id,
    async (id) => {
        try {
            schedules.value = await (await fetch(`http://localhost:5267/api/schedule/week/${userId}/course/${id}`)).json();
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
                <div v-for="date in slot[1]" style="display: flex; flex-direction: column;">
                    <p @click="() => console.log(date, date.getTime())" :class="[isSelected(date) ? 'active' : 'time-slot']">{{ date.toLocaleString(undefined, {
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

.active {
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