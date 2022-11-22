import { useUserService } from "../../hooks";
import { IClient } from "../../models";
import { generateColour, getInitials } from "../../services";
import { InfoTabs } from ".";

const ClientOverview = ({ client }: { client: IClient }) => {

  const { colour, createdAt, updatedAt, updatedBy } = client;
  const { getUser } = useUserService()

  return (
    <div className={`bg-gray-800/40 flex justify-between w-full mt-10 mb-4 rounded-lg text-gray-200 shadow-md space-x-8`}>
      <div className="flex m-5 pr-10 space-x-6 border-r-2 border-gray-700">
        <div style={{ backgroundColor: colour || generateColour() }} className="w-32 h-32 rounded-full shadow-lg">
          <span className="text-5xl inline-block font-bold tracking-wide relative top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2">
            {getInitials(`${client.firstName} ${client.lastName}`)}
          </span>
        </div>
        <div className="flex flex-col space-y-2">
          <div className="flex-grow">
            <h1 className="text-3xl font-bold">{client.firstName} {client.lastName}</h1>
            <span className="text-gray-400 tracking-wide text-sm">Client since: {new Date(createdAt || "").toLocaleDateString()}</span>
          </div>
          <div className="border-t border-gray-600 pt-2">
            <span className="text-gray-400 tracking-wide text-sm">
              Last updated: {new Date(updatedAt || "").toLocaleDateString()}
              <span> by <span className="font-medium px-2 py-1 bg-gray-800 tracking-wide rounded-lg select-none">
                {getUser(updatedBy)?.username || '--'}
              </span></span>
            </span>
          </div>
        </div>
      </div>
      <InfoTabs/>
    </div>
  )
}

export default ClientOverview;