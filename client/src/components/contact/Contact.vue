<script setup lang="ts">
import "../../style.css"
import Mail from "../icons/Mail.vue"
import Facebook from "../icons/social/Facebook.vue"
import Instagram from "../icons/social/Instagram.vue"
import Twitter from "../icons/social/Twitter.vue"
</script>

<template>
    <div class="flex h-screen">
        <div class="flex space flex-row justify-between w-10/12 m-auto items-center">
            <div class="w-6/12">
                <h1 class="font-medium mb-8">Let's Get in Touch!</h1>
                <h5 class="font-semibold mb-8">We'd love to hear from you! Drop us a message and we'll get back to you
                    soon!</h5>
                <p class="font-semibold"><span><Mail class="inline"/></span>example@gmail.com</p>
                <div class="flex flex-row gap-3">
                    <Facebook />
                    <Instagram />
                    <Twitter />
                </div>
            </div>

            <form class="flex flex-col bg-accent p-10 gap-4 rounded-2xl w-6/12 shadow-[0.75em_0.75em_lightgray]">
                <div>
                    <label class="font-semibold block" for="name">Name</label>
                    <input class="rounded-xl border-none w-full" type="text" name="name" id="name">
                </div>
                <div>
                    <label class="font-semibold block" for="email">Email</label>
                    <input class="rounded-xl border-none w-full" type="text" name="email" id="email">
                </div>
                <div>
                    <label class="font-semibold block" for="message">Message</label>
                    <textarea class="rounded-xl border-none w-full" name="message" rows="8"></textarea>
                </div>
                <button class="text-white rounded-full bg-primary py-2 w-1/3" type="submit">Send Message</button>
            </form>
        </div>
    </div>
</template>