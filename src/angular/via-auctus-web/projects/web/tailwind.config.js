/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "../ui/src/**/*.{html,ts}",
    "./src/**/*.{html,ts}",

  ],
  theme: {
    extend: {
      colors:{
        primary: {
          DEFAULT: "hsl(var(--primary))",
          foreground: "hsl(var(--primary-foreground))",
        },
        secondary: {
          DEFAULT: "hsl(var(--secondary))",
          foreground: "hsl(var(--secondary-foreground))",
        },
      }
    },
  },
  plugins: [],
}

