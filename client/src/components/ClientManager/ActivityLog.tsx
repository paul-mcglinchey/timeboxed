import { ActivityLogEntry } from ".";

const ActivityLog = () => {

  return (
    <div>
      {[].length > 0 ? (
        <div>
          {[].map((al, i) => (
            <ActivityLogEntry key={i} al={al} />
          ))}
        </div>
      ) : (
        <div>No activity logged for this client yet...</div>
      )}
    </div>
  )
}

export default ActivityLog;