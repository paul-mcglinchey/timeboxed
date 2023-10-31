import ActivityLog from './ActivityLog';

const infoTabs = [
  { title: "Activity Log", component: ActivityLog },
  { title: "Contact Info" },
  { title: "Contributors" },
]

const InfoTabs = () => {

  return (
    <div className="flex flex-grow flex-col space-y-4 max-w-md px-2 py-5">
      {infoTabs.map((it, i) => (
        <div key={i} className="-mt-2">
          <h3 className="text-xl text-gray-400 font-semibold tracking-wide mb-1">{it.title}</h3>
          <div className="bg-gray-600/10 rounded">
            {it.component ? (
              <it.component />
            ) : (
              <div>404</div>
            )}
          </div>
        </div>
      ))}
    </div>
  )
}

export default InfoTabs;