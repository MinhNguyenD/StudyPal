<template>
  <div class="flex">
    <SideBar/>
    <div class="min-h-screen w-full bg-white">
    <main class="py-16 w-full">
      <section class="flex flex-col md:flex-row text-center md:text-left mb-16 p-12 items-center text-black px-16">
        <div class="md:w-1/2">
          <h1 class="font-bold mb-8">Find your <span class="text-blue">StudyPal</span> within a minute</h1>
          <h6 class="mb-8">Unlock your full potential, with the power of teamwork and expert guidance. Join us to
            elevate your learning through collaboration.</h6>
          <button class="bg-blue text-white px-6 py-3 rounded hover:bg-dark-blue">Get Started</button>
        </div>
        <div class="md:w-1/2 mt-8 md:mt-0">
          <img :src="mainImage1" alt="Main Image" class="mx-auto" />
        </div>
      </section>

      <section class="bg-blue text-white p-12 rounded mb-16">
        <h2 class="font-bold mb-8 text-left">What We Offer</h2>
        <div class="grid grid-cols-1 md:grid-cols-3 gap-12">
          <div class="bg-light-blue p-8 rounded shadow text-black">
            <img :src="calendarIcon1" alt="Feature Logo" class="h-12 w-12 mb-6 mx-auto" />
            <h3 class="font-bold mb-4 text-center">Schedule System</h3>
            <h6 class="text-center">Provide schedule system so you can view and edit your availability.</h6>
          </div>
          <div class="bg-light-blue p-8 rounded shadow text-black">
            <img :src="groupIcon1" alt="Feature Logo" class="h-12 w-12 mb-6 mx-auto" />
            <h3 class="font-bold mb-4 text-center">Study Group</h3>
            <h6 class="text-center">Create or join a study group session of your courses.</h6>
          </div>
          <div class="bg-light-blue p-8 rounded shadow text-black">
            <img :src="groupIcon2" alt="Feature Logo" class="h-12 w-12 mb-6 mx-auto" />
            <h3 class="font-bold mb-4 text-center">Tutor</h3>
            <h6 class="text-center">Find a tutor or become one for your courses.</h6>
          </div>
        </div>
      </section>

      <section class="p-12 px-16">
        <h2 class="font-bold mb-8 text-black text-left">Why Choose StudyPal</h2>
        <div class="flex flex-col md:flex-row md:items-center">
          <div class="flex-1 mb-8 md:mb-0 md:mr-8">
            <img :src="mainImage2" alt="Why Choose Us Image" />
          </div>
          <div class="flex-1 space-y-6 text-black text-left">
            <div>
              <h3 class="font-bold">Personalized Learning</h3>
              <h6>Access and tailor the learning experience to what you need from the course you choose.</h6>
            </div>
            <div>
              <h3 class="font-bold">Enhance Productivity</h3>
              <h6>Studying with others helps boost motivation and productivity to stay on track of your learning goals.
              </h6>
            </div>
            <div>
              <h3 class="font-bold">Networking Opportunities</h3>
              <h6>Connect with like-minded individuals, build relationships, and expand your academic and professional
                network.</h6>
            </div>
          </div>
        </div>
      </section>
    </main>
  </div>
  </div>
  
</template>

<script>
import mainImage1 from "../assets/main-image-1.svg";
import mainImage2 from "../assets/main-image-2.svg";
import groupIcon1 from "../assets/group-icon-1.png";
import groupIcon2 from "../assets/group-icon-2.png";
import calendarIcon1 from "../assets/calendar-icon-1.png";
import SideBar from "./SideBar.vue";

export default {
  name: "LandingPage",
  data() {
    return {
      mainImage1,
      groupIcon1,
      groupIcon2,
      calendarIcon1,
      mainImage2
    };
  },
  components: {
    SideBar
  }
};
</script>

<style>
:root {
  --dark-blue: #001f3f;
  --blue: #007bff;
  --light-blue: #d8eefe;
  --orange: #ffc107;
  --light-gray: #f8f9fa;
  --white: #ffffff;
}

body {
  font-family: 'Roboto', sans-serif;
}

.bg-dark-blue {
  background-color: var(--dark-blue);
}

.bg-blue {
  background-color: var(--blue);
}

.bg-light-blue {
  background-color: var(--light-blue);
}

.text-dark-blue {
  color: var(--dark-blue);
}

.text-blue {
  color: var(--blue);
}

.text-light-blue {
  color: var(--light-blue);
}
</style>
