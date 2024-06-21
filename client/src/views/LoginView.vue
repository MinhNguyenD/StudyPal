<template>
  <NavBar />
  <LoginForm />
</template>

<script>
  import LoginForm from '../components/LoginForm.vue'
  import NavBar from '../components/NavBar.vue'

    export default {
        components: {
            LoginForm,
            NavBar
        },
    };
</script>