/** @type {import('tailwindcss').Config} */
export default {
  content: [
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
  plugins: [],
}

