<template>
  <NavBar />
  <RegistrationForm />
</template>

<script>
  import RegistrationForm from '../components/RegistrationForm.vue'
  import NavBar from '../components/NavBar.vue'

    export default {
        components: {
            RegistrationForm,
            NavBar
        },
    };
</script>