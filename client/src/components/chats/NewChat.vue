<template>
    <div class="relative inline-block">
        <button @click="toggleSearchBox" class="p-1 bg-blue-500 text-white rounded-full">
            <img :src="PlusIcon" class="w-5" alt="new-chat">
        </button>

        <div v-if="isSearchBoxOpen" class="absolute left-0 mt-2 bg-white p-4 rounded shadow-lg w-64">
            <input type="text" v-model="searchQuery" @input="searchUsernames" placeholder="Search usernames..."
                class="w-full p-2 border rounded mb-2" />
            <ul>
                <li v-for="user in filteredUsernames" :key="user" @click="openChatWindow(user)"
                    class="p-2 cursor-pointer hover:bg-gray-200">
                    {{ user }}
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import PlusIcon from '../../assets/plus-icon-1.svg'

export default {
    props: {
        currentUserId: {
            type: String,
            required: true
        }
    },
    data() {
        return {
            PlusIcon,
            isSearchBoxOpen: false,
            searchQuery: '',
            profiles: [],
            usernames: [],
            filteredUsernames: []
        };
    },
    methods: {
        toggleSearchBox() {
            this.isSearchBoxOpen = !this.isSearchBoxOpen;
            this.searchQuery = '';
            this.filteredUsernames = [];
            this.fetchAllUsers();
        },
        searchUsernames() {
            if (this.searchQuery.trim() === '') {
                this.filteredUsernames = [];
                return;
            }

            const query = this.searchQuery.toLowerCase();
            this.filteredUsernames = this.usernames.filter(user =>
                user.toLowerCase().includes(query)
            );
        },
        openChatWindow(user) {
            const name = user.split(" : ")[0];
            this.profiles.map(profile => {
                if (profile.username === name) {
                    this.$emit('open-chat', { username: profile.username, currentUserId: this.currentUserId });
                    this.isSearchBoxOpen = false;
                }
            });
        },
        async fetchAllUsers() {
            try {
                const response = await axios.get('/user/profiles');
                this.profiles = response.data;
                this.sortUsernames();
            } catch (error) {
                console.error('Error fetching profiles:', error);
            }
        },
        sortUsernames() {
            this.usernames = this.profiles.map(user => `${user.username} : ${user.firstName} ${user.lastName}`);
        }
    }
};
</script>

<style scoped></style>
