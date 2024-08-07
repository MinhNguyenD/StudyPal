<template>
  <div class="w-2/3 flex flex-col">
    <div v-if="selectedConversation?.id" class="flex-1 flex flex-col">
      <div class="flex items-center p-4 border-b border-gray-300 bg-zinc-300 rounded-tr-2xl">
        <div class="w-10 h-10 rounded-full bg-zinc-500 text-white flex items-center justify-center mr-3">
          {{ selectedConversation.id.toUpperCase()[0] }}
        </div>
        <div>
          <p v-if="selectedConversation.type === 'UserMessage'" class="text-gray-800 font-bold">{{
            selectedConversation.firstname + " " + selectedConversation.lastname }}</p>
          <p v-if="selectedConversation.type === 'GroupMessage'" class="text-gray-800 font-bold">{{
            selectedConversation.id }}</p>
          <p v-if="selectedConversation.type === 'UserMessage'" class="text-gray-600">{{ selectedConversation.id }}</p>
          <p v-if="selectedConversation.type === 'GroupMessage'" class="text-gray-600">Group Chat</p>
        </div>
      </div>

      <div class="flex-1 p-4 overflow-y-auto bg-zinc-100 messages-container" ref="messagesContainer">
        <div v-for="message in selectedConversation.messages" :key="message.time"
          :class="{ 'text-right': message.senderId === currentUserId, 'text-left': message.senderId !== currentUserId }">
          <span class="text-xs mx-4 block italic">{{ message.senderId }} | {{ formatTime(message.time) }}</span>

          <div
            :class="{ 'bg-blue-500 text-white': message.senderId === currentUserId, 'bg-gray-200 text-black': message.senderId !== currentUserId }"
            class="inline-block px-5 py-3 rounded-full">
            <p>{{ message.message }}</p>
          </div>
        </div>
      </div>

      <div class="p-4 border-t flex items-center bg-zinc-100 sticky bottom-0 rounded-br-2xl">
        <input :value="newMessage" @input="updateNewMessage" @keyup.enter="sendMessage" placeholder="Write a message"
          class="flex-1 py-3 px-5 border border-gray-300 rounded-full mr-2" />
        <button @click="sendMessage" class="bg-blue-500 text-white p-3 rounded-full">
          <img :src="SendIcon" alt="sendIcon-icon" class="w-8" />
        </button>
      </div>
    </div>

    <div v-else class="flex-1 flex flex-col bg-zinc-100 rounded-tr-2xl rounded-br-2xl">
      <div class="flex-1 p-4 flex items-center justify-center">
        <p class="text-center text-gray-500 italic">Select a chat to start messaging</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import SendIcon from '../../assets/send-icon.png';
import { Conversation } from '@/models/Conversation';

export default defineComponent({
  props: {
    selectedConversation: Object as PropType<Conversation | null>,
    currentUserId: {
      type: String,
      required: true
    },
    newMessage: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      SendIcon
    };
  },
  methods: {
    sendMessage() {
      this.$emit('sendMessage');
    },
    updateNewMessage(event: Event) {
      this.$emit('updateNewMessage', (event.target as HTMLInputElement).value);
    },
    formatTime(time: string) {
      const date = new Date(time);
      return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    },
    scrollToEnd() {
      this.$nextTick(() => {
        const container = this.$refs.messagesContainer as HTMLElement;
        if (container) {
          container.scrollTop = container.scrollHeight;
        }
      });
    }
  },
  watch: {
    selectedConversation: {
      handler() {
        this.scrollToEnd();
      },
      deep: true
    }
  },
  mounted() {
    this.scrollToEnd();
  }
});
</script>

<style scoped>
.messages-container {
  max-height: calc(92vh - 215px);
}

.bg-zinc-100 {
  background-color: #f3f4f6;
}
</style>
