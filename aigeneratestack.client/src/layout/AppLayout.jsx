import { useEffect, useState } from "react";

export default function AppLayout({ children }) {
  const [dark, setDark] = useState(false);

  useEffect(() => {
    document.documentElement.classList.toggle("dark", dark);
  }, [dark]);

  return (
    <div className="min-h-screen bg-gradient-to-br from-indigo-50 via-white to-purple-50 dark:from-gray-900 dark:via-gray-950 dark:to-gray-900 transition-colors">
      {/* 🔥 BACKGROUND GLOW (ADD THIS HERE) */}
      <div className="absolute inset-0 
        bg-[radial-gradient(circle_at_top,rgba(99,102,241,0.15),transparent_60%)]
        pointer-events-none"
      />

      {/* HEADER */}
      <header className="flex items-center justify-between px-8 py-4">
        <div className="flex items-center gap-3 text-2xl font-bold text-gray-900 dark:text-white">
          🎬 AI Video Studio
        </div>

        <button
          onClick={() => setDark(!dark)}
          className="px-4 py-2 rounded-xl border bg-white dark:bg-gray-800 dark:text-white shadow"
        >
          {dark ? "🌙 Dark" : "☀️ Light"}
        </button>
      </header>

      {/* PAGE CONTENT */}
      <main className="flex items-center justify-center">
        {children}
      </main>
    </div>
  );
}


