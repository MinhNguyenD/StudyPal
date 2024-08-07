<script setup lang="ts">
import SideBar from "@/components/SideBar.vue";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion";

const defaultValue = "item-1";

const accordionGeneralItems = [
  {
    value: "item-1",
    title: "What is StudyPal?",
    content:
      "StudyPal is a platform designed to help students connect with study partners and tutors based on their schedules. It facilitates the formation of study groups and tutoring sessions, enhancing academic collaboration and support.",
  },
  {
    value: "item-2",
    title: "How does StudyPal work?",
    content:
      "Students can input their schedules and availability into the app. The app then matches students with similar schedules for study groups or finds available tutors for tutoring sessions. The goal is to make it easy for students to coordinate and collaborate effectively.",
  },
];

const accordionCourseScheduleItems = [
  {
    value: "item-1",
    title: "How do I find my course?",
    content:
      'After logging in, go to the "Dashboard" section. Here you can search your course in the search bar or find it on the Dashboard feed.',
  },
  {
    value: "item-2",
    title: "How do I input my schedule?",
    content:
      "After logging in and finding your wanted course, go to the course information page by selecting the course title link. Here you can find a common availability schedule, where you can select the time slots that you are available.",
  },
];

const accordionStudyGroupItems = [
  {
    value: "item-1",
    title: "How do I join a study group?",
    content:
      'After selecting the time slots in the common availability schedule of a course, the app will display a list of study groups that are available in that time. You can request to join the group by selecting "Request To Join" button. You will be in the study group when the group owner approve your request.',
  },
  {
    value: "item-2",
    title: "Can I create my own study group?",
    content:
      'Yes, you can create your own study group by going to a specific course and selecting "Create Group." You will need to provide information about your group.',
  },
];

const accordionTutorItems = [
  {
    value: "item-1",
    title: "How do I find a tutor?",
    content:
      'After finding your wanted course and navigating to the course information page, turn the switch to "Tutor" option. Here you will find a common availability schedule of tutors, where you can select the time slots that you are available. You can select a suitable tutor and request to join their tutor session by clicking "Request To Join" button. You will be in the tutor session when the tutor approve your request.',
  },
  {
    value: "item-2",
    title: "Can I become a tutor?",
    content:
      'Yes, if you are interested in becoming a tutor, you can edit your account profile and add "Tutor" under "Roles". You will need to provide information about your qualifications and availability. After that, students can start booking sessions with you.',
  },
];
</script>

<template>
  <div class="flex">
    <SideBar />
    <div class="container">
      <div>
        <h1 class="text-3xl font-bold text-center">
          Frequently Asked Questions (FAQ)
        </h1>
        <br />

        <h1 class="text-2xl font-bold">General</h1>
        <Accordion type="single" class="w-full" collapsible :default-value="defaultValue">
          <AccordionItem v-for="item in accordionGeneralItems" :key="item.value" :value="item.value">
            <AccordionTrigger>{{ item.title }}</AccordionTrigger>
            <AccordionContent>
              {{ item.content }}
            </AccordionContent>
          </AccordionItem>
        </Accordion>
        <br />
        <h1 class="text-2xl font-bold">Courses and Schedules</h1>
        <Accordion type="single" class="w-full" collapsible :default-value="defaultValue">
          <AccordionItem v-for="item in accordionCourseScheduleItems" :key="item.value" :value="item.value">
            <AccordionTrigger>{{ item.title }}</AccordionTrigger>
            <AccordionContent>
              {{ item.content }}
            </AccordionContent>
          </AccordionItem>
        </Accordion>
        <br />
        <h1 class="text-2xl font-bold">Study Groups</h1>
        <Accordion type="single" class="w-full" collapsible :default-value="defaultValue">
          <AccordionItem v-for="item in accordionStudyGroupItems" :key="item.value" :value="item.value">
            <AccordionTrigger>{{ item.title }}</AccordionTrigger>
            <AccordionContent>
              {{ item.content }}
            </AccordionContent>
          </AccordionItem>
        </Accordion>

        <br />
        <h1 class="text-2xl font-bold">Tutors</h1>
        <Accordion type="single" class="w-full" collapsible :default-value="defaultValue">
          <AccordionItem v-for="item in accordionTutorItems" :key="item.value" :value="item.value">
            <AccordionTrigger>{{ item.title }}</AccordionTrigger>
            <AccordionContent>
              {{ item.content }}
            </AccordionContent>
          </AccordionItem>
        </Accordion>
      </div>
    </div>
  </div>
</template>
