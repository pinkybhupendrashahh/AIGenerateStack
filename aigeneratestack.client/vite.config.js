import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default {
  darkMode: "class", // 👈 REQUIRED
  content: ["./index.html", "./src/**/*.{js,jsx}"],
  theme: {
    extend: {},
  },
  plugins: [],
};
