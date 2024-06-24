<script setup lang="ts">
import NavBar from './components/NavBar.vue';

</script>

<template>
  <NavBar/>
  <router-view/>
  <h1>Don't render here</h1>
</template>