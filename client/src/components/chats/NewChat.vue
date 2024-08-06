<template>
    <div class="relative inline-block">
        <button @click="toggleSearchBox" class="p-1 bg-blue-500 text-white rounded-full">
            <img :src="PlusIcon" class="w-5" alt="new-chat">
        </button>

        <div v-if="isSearchBoxOpen" class="absolute left-0 mt-2 bg-white p-4 rounded shadow-lg w-64">
            <input type="text" v-model="searchQuery" @input="searchUsernames" placeholder="Search usernames..."
                class="w-full p-2 border rounded mb-2" />
            <ul class="overflow-y-auto h-min">
                <li v-for="user in filteredUsernames" :key="user" @click="openChatWindow(user)"
                    class="p-2 cursor-pointer hover:bg-gray-200">
                    {{ user }}
                </li>
            </ul>
        </div>
    </div>
</template>

<script lang='ts'>
import axios from 'axios';
import PlusIcon from '../../assets/plus-icon-1.svg'
import { Conversation, MessageType } from '@/models/Conversation';
import { UserProfile } from '@/models/user';

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
            profiles: [] as UserProfile[],
            conversations: [] as Conversation[],
            usernames: [] as string[],
            filteredUsernames: [] as string[]
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
        openChatWindow(user: string) {
            const name = user.split(" : ")[0];
            this.conversations.map(profile => {
                if (profile.id === name) {
                    this.$emit('open-chat', { username: profile.id });
                    this.isSearchBoxOpen = false;
                }
            });
        },
        async fetchAllUsers() {
            try {
                const response = await axios.get('api/Userprofile/profiles/not/' + this.currentUserId);
                this.profiles = response.data;
                this.conversations = this.setCoversations(this.profiles);
                this.sortUsernames();
            } catch (error) {
                console.error('Error fetching profiles:', error);
            }
        },
        setCoversations(values: UserProfile[]): Conversation[] {
            return values.map(user => ({
                id: user.username,
                firstname: user.firstName,
                lastname: user.lastName,
                messages: [],
                type: MessageType.UserMessage
            }));
        },
        sortUsernames() {
            this.usernames = this.conversations.map(user => `${user.id} : ${user.firstname} ${user.lastname}`);
        }
    }
};
</script>

<style scoped></style>
