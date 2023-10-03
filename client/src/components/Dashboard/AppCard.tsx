import { ChevronRightIcon } from "@heroicons/react/24/solid";
import { Link } from "react-router-dom";
import { combineClassNames } from "../../services";
import { createRef, useEffect } from "react";

interface IAppCardProps {
  title: string,
  subtitle?: string | undefined,
  href: string,
  backgroundImage?: string | undefined
  backgroundVideo?: string | undefined
}

const AppCard = ({ title, subtitle, href, backgroundImage, backgroundVideo }: IAppCardProps) => {

  const videoRef = createRef<HTMLVideoElement>()

  useEffect(() => {
    videoRef.current?.load();
  }, [backgroundVideo])

  return (
    <Link to={href}>
      <div className={combineClassNames(
        "h-full rounded-xl shadow-md group relative overflow-clip"
      )}>
        <div className="w-full h-full flex flex-col px-6 md:px-12 py-8 rounded-xl bg-black/40">
          <div>
            <h1 className="text-3xl md:text-5xl text-white text-left font-bold tracking-wide">{title}</h1>
            <hr className="mt-4 mb-2 border-b-2" />
            <span className="font-bold text-white text-lg md:text-xl tracking-wide">{subtitle}</span>
            <div className="flex justify-end mt-4">
              <div className="text-white p-1 border-2 rounded-full transition-transform group-hover:scale-110"><ChevronRightIcon className="w-6 h-6" /></div>
            </div>
          </div>
        </div>
        {backgroundVideo && (
          <video ref={videoRef} autoPlay muted loop className="absolute top-0 left-0 -z-10 object-cover w-full h-full">
            <source src={backgroundVideo} type="video/mp4" />
          </video>
        )}
        {!backgroundVideo && backgroundImage && (
          <img alt={title} src={backgroundImage} className="absolute top-0 left-0 -z-10 object-cover w-full h-full" />
        )}
      </div>
    </Link>
  )
}

export default AppCard;