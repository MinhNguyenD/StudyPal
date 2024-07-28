<template>
  <div class="flex overflow-hidden">
    <SideBar />

    <div class="flex w-full flex-1 p-10 mainWindow">
      <ChatBar
        :conversations="conversations"
        :selectedConversation="selectedConversation"
        :currentUserId="currentUserId"
        @selectConversation="selectConversation"
        @open-chat="openChatWindow"
      />
      <ChatWindow
        :selectedConversation="selectedConversation"
        :currentUserId="currentUserId"
        :newMessage="newMessage"
        @sendMessage="sendMessage"
        @updateNewMessage="updateNewMessage"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "axios";
import ChatWindow from "./ChatWindow.vue";
import ChatBar from "./ChatBar.vue";
import SideBar from "../SideBar.vue";
import { useUserStore } from "@/store/user";
import { User } from "@/models/user";
import { Conversation, Message, MessageType } from "@/models/Conversation";

export default defineComponent({
  components: {
    ChatBar,
    ChatWindow,
    SideBar,
  },
  setup() {
    const userStore = useUserStore();
    return {
      userStore,
    };
  },
  data() {
    return {
      user: null as User | null,
      currentUserId: "",
      newMessage: "",
      messages: [] as Message[],
      conversations: [] as Conversation[],
      selectedConversation: null as Conversation | null,
      refreshInterval: null as ReturnType<typeof setInterval> | null,
      email: this.$route.query.email || "",
    };
  },
  methods: {
    async fetchAllMessages() {
      try {
        const response = await axios.get(
          "api/ChatMessage/messages/" + this.currentUserId
        );
        this.messages = response.data;
        this.conversations = this.getUniqueUserIds();
        if (this.selectedConversation?.id) {
          this.selectConversation(this.selectedConversation);
        }
      } catch (error) {
        console.log(error);
      }
    },
    getUniqueUserIds(): Conversation[] {
      const userMap = new Map<string, string>();

      this.messages.forEach((message) => {
        if (
          message.senderId !== this.currentUserId &&
          this.filterMessagesBySelectedUser(message.senderId).length !== 0
        ) {
          if (
            !userMap.has(message.senderId) ||
            new Date(userMap.get(message.senderId)!) < new Date(message.time)
          ) {
            userMap.set(message.senderId, message.time);
          }
        }
        if (
          message.receiverId !== this.currentUserId &&
          this.filterMessagesBySelectedUser(message.receiverId).length !== 0
        ) {
          if (
            !userMap.has(message.receiverId) ||
            new Date(userMap.get(message.receiverId)!) < new Date(message.time)
          ) {
            userMap.set(message.receiverId, message.time);
          }
        }
      });

      const userArray = Array.from(userMap, ([id, time]) => ({ id, time }));

      userArray.sort(
        (a, b) => new Date(b.time).getTime() - new Date(a.time).getTime()
      );

      return userArray.map((user) => ({
        id: user.id,
        name: this.getConversationName(user.id),
        messages: this.filterMessagesBySelectedUser(user.id),
        type: user.id.startsWith("group")
          ? MessageType.GroupMessage
          : MessageType.UserMessage,
      }));
    },
    getConversationName(userId: string): string {
      const user = this.conversations.find(
        (conversation) => conversation.id === userId
      );
      return user ? user.name || userId : userId;
    },
    getLastMessage(userId: string): Message | {} {
      const filteredMessages = this.messages.filter(
        (message) =>
          (message.senderId === this.currentUserId &&
            message.receiverId === userId) ||
          (message.receiverId === this.currentUserId &&
            message.senderId === userId) ||
          message.type == MessageType.GroupMessage
      );
      return (
        filteredMessages.sort(
          (a, b) => new Date(b.time).getTime() - new Date(a.time).getTime()
        )[0] || {}
      );
    },
    selectConversation(conversation: Conversation) {
      this.selectedConversation = {
        id: conversation.id,
        name: conversation.name,
        messages: this.filterMessagesBySelectedUser(conversation.id),
        type: conversation.type,
      };
    },
    filterMessagesBySelectedUser(selectedUserId: string): Message[] {
      return this.messages.filter(
        (message) =>
          (message.senderId === this.currentUserId &&
            message.receiverId === selectedUserId) ||
          (message.receiverId === this.currentUserId &&
            message.senderId === selectedUserId) ||
          message.receiverId === selectedUserId
      );
    },
    async sendMessage() {
      if (this.newMessage.trim() === "") return;
      const messageType = this.selectedConversation!.type;
      const message: Message = {
        senderId: this.currentUserId,
        receiverId: this.selectedConversation!.id,
        message: this.newMessage,
        time: new Date().toISOString(),
        type: messageType,
      };

      try {
        await axios.post("api/ChatMessage/send", message);
        this.selectedConversation!.messages.push({
          ...message,
          senderId: this.currentUserId,
        });
        this.newMessage = "";
        this.scrollToEnd();
      } catch (error) {
        console.log(error);
      }
    },
    updateNewMessage(newMessage: string) {
      this.newMessage = newMessage;
    },
    scrollToEnd() {
      this.$nextTick(() => {
        const container = this.$refs.messagesContainer as HTMLElement | null;
        if (container) {
          container.scrollTop = container.scrollHeight;
        }
      });
    },
    openChatWindow(value: {username: string}) {

      console.log(value);
      const conversation: Conversation = {
        id: value.username,
        name: value.username,
        messages: [],
        type: MessageType.UserMessage,
      };
      this.scrollToEnd();
      this.selectConversation(conversation);
    },
    startMessageRefresh() {
      this.refreshInterval = setInterval(this.fetchAllMessages, 5000);
    },
    stopMessageRefresh() {
      if (this.refreshInterval) {
        clearInterval(this.refreshInterval);
      }
    },
  },
  async mounted() {
    this.user = this.userStore.storedUser;
    if (this.user?.username != null) {
      this.currentUserId = this.user.username;
      this.fetchAllMessages();
      this.scrollToEnd();
      this.startMessageRefresh();
    }
  },
  beforeUnmount() {
    this.stopMessageRefresh();
  },
});
</script>

<style>
.mainWindow {
  height: fit-content;
}
</style>
