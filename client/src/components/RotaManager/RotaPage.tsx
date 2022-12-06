import { useParams } from "react-router";
import { RotaContext, ScheduleProvider } from "../../contexts";
import { Scheduler } from ".";
import { IRota } from "../../models";
import { useContext, useEffect, useState } from "react";

const RotaPage = () => {

  const [rota, setRota] = useState<IRota>()
  const { rotaId } = useParams()
  const { getRota } = useContext(RotaContext)

  useEffect(() => {
    if (rotaId) setRota(getRota(rotaId))
  }, [rotaId, getRota])

  return (
    <>
      {rota ? (
        <ScheduleProvider rotaId={rota.id} >
          <Scheduler rota={rota} />
        </ScheduleProvider >
      ) : (
        <>
          Loading dat Rota
        </>
      )}
    </>
  )
}

export default RotaPage