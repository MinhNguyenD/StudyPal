export interface ScheudleItem {
    id: string;
    userId: string;
    timeFrom: number;
    timeTo: number;
    courseId: string;
}

export interface Course {
    id: string;
    courseCode: string;
    courseName: string;
}