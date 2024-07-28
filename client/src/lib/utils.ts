import { type ClassValue, clsx } from 'clsx'
import { twMerge } from 'tailwind-merge'
// TODO: refactor to be able to pass in a date so these work for any date instead of the current one
export const ms = 1;
export const secs = 1000 * ms;
export const mins = 60 * secs;
export const hours = 60 * mins;
export const days = 24 * hours;

// returns a timestamp to 12 am of today
export function getToday() {
  const date = new Date();
  return new Date(date.getTime() -
  ((date.getHours() * hours) +
  (date.getMinutes() * mins) +
  (date.getSeconds() * secs) +
  (date.getMilliseconds())))
}

// always returns a timestamp to the last sunday
export function getWeekStart() {
  return new Date(getToday().getTime() - new Date().getDay() * days)
}

// returns a timestamp to the coming saturday 23:59:59
export function getTheWeeknd() {
  return new Date(getWeekStart().getTime() + (7 * days))
}

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}