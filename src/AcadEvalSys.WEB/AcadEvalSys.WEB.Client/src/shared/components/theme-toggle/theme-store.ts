import { create } from "zustand";
import { persist } from "zustand/middleware";

type Theme = "dark" | "light" | "system";

interface ThemeState {
  theme: Theme;
  setTheme: (theme: Theme) => void;
  getSystemTheme: () => "dark" | "light";
  getResolvedTheme: () => "dark" | "light";
  applyTheme: (theme: Theme) => void;
  initializeTheme: () => void;
}

export const useTheme = create<ThemeState>()(
  persist(
    (set, get) => ({
      theme: "system",

      setTheme: (theme: Theme) => {
        set({ theme });
        get().applyTheme(theme);
      },

      getSystemTheme: () => {
        if (typeof window === "undefined") return "light";
        return window.matchMedia("(prefers-color-scheme: dark)").matches
          ? "dark"
          : "light";
      },

      getResolvedTheme: () => {
        const { theme, getSystemTheme } = get();
        return theme === "system" ? getSystemTheme() : theme;
      },

      applyTheme: (theme: Theme) => {
        if (typeof window === "undefined") return;

        const root = window.document.documentElement;
        root.classList.remove("light", "dark");

        const resolvedTheme =
          theme === "system" ? get().getSystemTheme() : theme;
        root.classList.add(resolvedTheme);
      },

      initializeTheme: () => {
        const { theme, applyTheme } = get();

        // Apply current theme
        applyTheme(theme);

        // Listen for system theme changes
        if (typeof window !== "undefined") {
          const mediaQuery = window.matchMedia("(prefers-color-scheme: dark)");

          const handleChange = () => {
            const currentTheme = get().theme;
            if (currentTheme === "system") {
              applyTheme("system");
            }
          };

          mediaQuery.addEventListener("change", handleChange);

          // Cleanup function (you can call this when needed)
          return () => mediaQuery.removeEventListener("change", handleChange);
        }
      },
    }),
    {
      name: "theme-storage",
      partialize: (state) => ({ theme: state.theme }),
    }
  )
);

if (typeof window !== "undefined") {
  setTimeout(() => {
    useTheme.getState().initializeTheme();
  }, 0);
}
