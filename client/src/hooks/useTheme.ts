import { useEffect, useState } from "react"

const useTheme = () => {
  const [theme, setTheme] = useState<string>(
    localStorage["theme"] ? localStorage["theme"] : window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
  )

  useEffect(() => {
    // Update this to use an enum and just remove all possible themes on each update
    document.documentElement.classList.remove('dark', 'light')

    updateTheme(theme)
  }, [theme])

  const updateTheme = (theme: string) => {
    localStorage["theme"] = theme
    document.documentElement.classList.add(theme)
  }

  return { theme, setTheme }
}

export default useTheme
