<script setup lang="ts">
import { ref } from "vue";
import "../../style.css"
import { server } from "../../instance.ts"

const emailValidate = /^[a-zA-Z0-9_]+(-|.)*[a-zA-Z0-9_]*@[a-zA-Z0-9_]+(-|.)*[a-zA-Z0-9_]*\.[a-zA-Z0-9_]+(-|.)*[a-zA-Z0-9_]*$/;

const name = defineModel<string>("name");
const email = defineModel<string>("email");
const message = defineModel<string>("message");

const errors = ref<{ name: boolean, email: boolean, message: boolean }>({ name: false, email: false, message: false })

async function submit() {
    errors.value.email = !emailValidate.test(email.value || "");
    errors.value.message = (message.value?.trim().length || 0) < 3;
    errors.value.name = (name.value?.trim().length || 0) < 2;
    await server.post("/UserContact", { name, email, message });
    // TODO: send request
}
</script>

<template>
    <div class="flex h-screen">
        <div class="flex space flex-row justify-between w-10/12 m-auto items-center">
            <div class="w-6/12">
                <h1 class="font-medium mb-8">Let's Get in Touch!</h1>
                <h4 class="font-semibold mb-6">We'd love to hear from you! Drop us a <br />message and we'll get back to
                    you
                    soon!</h4>
                <p class="font-semibold mb-2"><span class="mr-2"><img style="pointer-events: none;" src="/Mail.svg"
                            class="inline" /></span>example@gmail.com</p>
                <div class="flex flex-row gap-3">
                    <img style="pointer-events: none;" src="/Facebook.svg" />
                    <img style="pointer-events: none;" src="/Instagram.svg" />
                    <img style="pointer-events: none;" src="/Twitter.svg" />
                </div>
            </div>

            <form class="flex flex-col bg-accent p-10 gap-4 rounded-2xl w-6/12 shadow-[0.75em_0.75em_lightgray]">
                <div>
                    <label class="font-semibold block" for="name">Name</label>
                    <input v-bind:class="errors.name ? 'border-error' : 'border-none'" class="rounded-xl w-full"
                        type="text" name="name" id="name" v-model="name">
                </div>
                <div>
                    <label class="font-semibold block" for="email">Email</label>
                    <input v-bind:class="errors.email ? 'border-error' : 'border-none'" class="rounded-xl w-full"
                        type="text" name="email" id="email" v-model="email">
                </div>
                <div>
                    <label class="font-semibold block" for="message">Message</label>
                    <textarea v-bind:class="errors.message ? 'border-error' : 'border-none'" class="rounded-xl w-full"
                        name="message" rows="8" v-model="message"></textarea>
                </div>
                <button class="text-white rounded-full bg-primary py-2 w-1/3" type="submit" @click.prevent="submit">Send
                    Message</button>
            </form>
        </div>
    </div>
</template>
