<script setup lang="ts">
import NavBar from './components/NavBar.vue'
import LandingPage from './components/Homepage.vue'
import FAQ from './views/FAQ.vue';
</script>

<template>
  <NavBar/>
  <router-view/>
  <h1>TODO: make a footer</h1>
</template>