<script lang="ts">
import { PropType } from "vue";
import { useUserStore } from "@/store/user";
import { hours } from "@/lib/utils";
import { useRoute } from "vue-router";
import axios from "axios";
import { GroupChat, Message, MessageType } from "@/models/Conversation";
type SlotData =
  {
    isSelected: true;
    id: string;
    users: string[];
  }
  | {
    isSelected: false;
  };
type Slot = { start: Date, data: SlotData }

export default {
  setup() {
    const userStore = useUserStore();
    const uid = userStore.storedUser!.id
    const username = userStore.storedUser!.username
    const route = useRoute()
    const courseId = route.params.id
    return { userStore, uid, username, route, courseId };
  },
  props: {
    slotData: {
      type: Object as PropType<Slot>,
      required: true
    },
    revalidate: {
      type: Function,
      required: true,
    },
  },
  data() {
    return {
      requestedGroups: [],
    };
  },
  computed: {
    isInGroup() {
      if (this.slotData.data.isSelected) {
        console.log(this.uid)
        return this.slotData.data.users.includes(this.uid);
      }
    }
  },
  methods: {
    formatDate(date: Date): string {
      const options: Intl.DateTimeFormatOptions = { month: 'long', day: 'numeric', year: 'numeric' };
      return date.toLocaleDateString('en-US', options);
    },
    formatTime(start: Date, end: Date): string {
      const options: Intl.DateTimeFormatOptions = { hour: 'numeric', minute: 'numeric' };
      const startTime = start.toLocaleTimeString('en-US', options);
      const endTime = end.toLocaleTimeString('en-US', options);
      return `${startTime} - ${endTime}`;
    },
    async joinGroup() {
      if (this.isInGroup) {
        return;
      }
      // Add to group chat
      const currentGroupId = this.createGroupChatId(this.slotData.start)
      await axios.post(`api/GroupChat/${currentGroupId}/users/${this.username}`);

      // Send message
      const message = `User ${this.username} just joined. Say Hi!`
      await this.sendMessage(currentGroupId, message)

      // create schedule
      await this.createSchedule();
    },
    async createGroup() {
      if (this.isInGroup) {
        return;
      }
      const newGroupId = this.createGroupChatId(this.slotData.start);

      const newGroup: GroupChat = {
        groupId: newGroupId,
        users: [this.username]
      }
      // Create group chat
      await axios.post("api/GroupChat", newGroup);

      // Send message
      const message = `Group ${newGroupId} was created by User ${this.username}. Say Hi!`
      await this.sendMessage(newGroupId, message)

      // create schedule
      await this.createSchedule();
    },
    async leaveGroup() {
      if (!this.isInGroup) {
        return;
      }
      const currentGroupId = this.createGroupChatId(this.slotData.start)

      // Send message
      const message = `User ${this.username} left the group :(`
      await this.sendMessage(currentGroupId, message)

      // remove from group chat
      await axios.delete(`api/GroupChat/${currentGroupId}/users/${this.username}`);
      await this.removeSchedule();
    },
    async createSchedule() {
      // Add to schedule aka create slot
      await axios.post("api/schedule", {
        schedule: [{
          timeFrom: this.slotData.start.getTime(),
          timeTo: this.slotData.start.getTime() + (1 * hours)
        }],
        courseId: this.courseId
      });
      this.revalidate(this.courseId);
    },
    async removeSchedule() {
      if (this.slotData.data.isSelected) {
        var timeFrom = this.slotData.start.getTime()
        var timeTo = timeFrom + (1 * hours)
        await axios.delete(`api/schedule/delete/${timeFrom}/${timeTo}/users/${this.uid}`);
        this.revalidate(this.courseId);
      }
    },
    createGroupChatId(date: Date): string {
      const dateOptions: Intl.DateTimeFormatOptions = { month: 'short', day: 'numeric', year: 'numeric' };
      const timeOptions: Intl.DateTimeFormatOptions = { hour: 'numeric', minute: 'numeric' };

      const formattedDate = new Intl.DateTimeFormat('en-US', dateOptions).format(date);
      const startTime = new Intl.DateTimeFormat('en-US', timeOptions).format(date);
      const endTime = new Intl.DateTimeFormat('en-US', timeOptions).format(new Date(date.getTime() + 60 * 60 * 1000));

      return `Group: ${this.courseId} - ${formattedDate} ${startTime} - ${endTime}`;
    },
    async sendMessage(groupId: string, message: string) {
      const messageType = MessageType.GroupMessage;
      const newMessage: Message = {
        senderId: this.uid,
        receiverId: groupId,
        message: message,
        time: new Date().toISOString(),
        type: messageType,
      };

      try {
        await axios.post("api/ChatMessage/send", newMessage);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>


<template>
  <div>
    <div class="py-10 space-y-2" v-if="slotData.data.isSelected">
      <p class="text-3xl font-semibold pb-2 border-b">Study Group:</p>
      <p class="text-xl pt-1">{{ courseId }}</p>
      <p class="text-xl">{{ formatDate(slotData.start) }}</p>
      <p class="text-xl">{{ formatTime(slotData.start, new Date(slotData.start.getTime() + 60 * 60 * 1000)) }}</p>
      <br>
      <p class="text-xl">Members: {{ slotData.data.users.length }}</p>
      <button :disabled="isInGroup" @click="joinGroup" :class="{ 'opacity-50 cursor-not-allowed': isInGroup }"
        class="bg-primary text-white py-2 px-4 rounded-full p-2">Join study group</button>
      <button v-if="isInGroup" @click="leaveGroup" class="mx-2 bg-red-500 text-white py-2 px-4 rounded-full p-2">Leave study
        group</button>
    </div>
    <div v-else class="py-10 w-60 space-y-2">
      <p class="text-3xl font-semibold pb-2 border-b">Study Group:</p>
      <p class="text-xl pt-1">{{ courseId }}</p>
      <p class="text-xl">{{ formatDate(slotData.start) }}</p>
      <p class="text-xl">{{ formatTime(slotData.start, new Date(slotData.start.getTime() + 60 * 60 * 1000)) }}</p>
      <br>
      <p class="text-xl font-semibold">There is no group in current time slot right now</p>
      <button :class="{ 'rounded-full opacity-50 cursor-not-allowed': isInGroup }" @click="createGroup" class="rounded-full bg-green-500 p-2 px-4">Create
        new group</button>
    </div>
  </div>
</template>
<style></style>