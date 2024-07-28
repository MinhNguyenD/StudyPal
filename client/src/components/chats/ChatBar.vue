<template>
  <div class="flex w-1/3 justify-around flex-col chatWindow">
    <div class="p-4 font-bold border rounded-tl-2xl flex justify-between">
      <h6 class="">Conversations</h6>
      <NewChat @open-chat="openChatWindow" :currentUserId="currentUserId" />
    </div>

    <div class="border-l h-full border-gray-300 bg-zinc-100 overflow-y-auto rounded-bl-2xl border-r">
      <div v-for="conversation in conversations" :key="conversation.id" @click="selectConversation(conversation)"
        class="flex items-center p-3 border-b border-gray-300 cursor-pointer hover:bg-gray-100"
        :class="{ 'bg-blue-100': conversation.id === selectedConversation?.id }">
        <div class="w-10 h-10 rounded-full bg-gray-400 text-white flex items-center justify-center mr-3">
          {{ conversation.id.toUpperCase()[0] }}
        </div>
        <div class="w-8/12">
          <p :class="{ 'font-bold': conversation.id === selectedConversation?.id }" class="text-gray-800">
            {{ conversation.name }}
          </p>
          <p :class="{ 'font-bold': conversation.id === selectedConversation?.id }" class="text-gray-500 text-sm">
            {{ formatMessage(getLastMessage(conversation.id).message) }}
          </p>
        </div>
        <div class="ml-auto w-20 text-right text-gray-400 text-xs">
          {{ formatTime(getLastMessage(conversation.id).time) }}
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import NewChat from '../chats/NewChat.vue';
import { Conversation, Message } from '@/models/Conversation';

export default defineComponent({
  props: {
    conversations: {
      type: Array as PropType<Conversation[]>,
      required: true
    },
    selectedConversation: {
      type: Object as PropType<Conversation | null>,
      required: true
    },
    currentUserId: {
      type: String,
      required: true
    }
  },
  components: {
    NewChat
  },
  methods: {
    selectConversation(conversation: Conversation) {
      this.$emit('selectConversation', conversation);
    },
    getLastMessage(userId: string): Message {
      const filteredMessages = (this.$parent as any).messages.filter(
        (message: Message) =>
          (message.senderId === this.currentUserId && message.receiverId === userId) ||
          (message.receiverId === this.currentUserId && message.senderId === userId)
      );
      return filteredMessages.sort((a: Message, b: Message) => new Date(b.time).getTime() - new Date(a.time).getTime())[0] || { senderId: '', receiverId: '', message: '', time: '' };
    },
    formatTime(time: string) {
      const date = new Date(time);
      return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    },
    openChatWindow({ username }: { username: string }) {
      this.$emit('open-chat', { username });
    },
    formatMessage(msg: string) {
      const maxLength = 25;
      const ellipsis = '...';

      if (msg.length > maxLength) {
        return msg.substring(0, maxLength) + ellipsis;
      } else {
        return msg;
      }
    }
  }
});
</script>

<style>
.chatWindow {
  height: 80vh;
}
</style>
