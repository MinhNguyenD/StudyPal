import typography from '@tailwindcss/typography';
import forms from '@tailwindcss/forms';
import aspectRatio from '@tailwindcss/aspect-ratio';

/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        "primary": "#007BFF",
        "accent": "#F2F0F0",
        "error": "#dc2626",
        "success": "#22c55e"
      }
    },
  },
  plugins: [
    typography,
    forms,
    aspectRatio,
  ],
