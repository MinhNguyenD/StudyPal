export interface Conversation {
    id: string;
    name?: string;
    messages: Message[];
    type: MessageType;
}

export interface Message {
    senderId: string;
    receiverId: string;
    message: string;
    time: string;
    type: MessageType;
}

export enum MessageType {
    UserMessage = 'UserMessage',
    GroupMessage = 'GroupMessage'
}