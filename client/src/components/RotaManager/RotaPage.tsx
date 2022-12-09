import { useParams } from "react-router";
import { ScheduleProvider } from "../../contexts";
import { IRota } from "../../models";
import { useEffect, useState } from "react";
import { SpinnerLoader } from "../Common";
import Scheduler from "./Scheduler";
import { useRotaService } from "../../hooks";
import { IApiError } from "../../models/error.model";

const RotaPage = () => {

  const [rota, setRota] = useState<IRota>()
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<IApiError>()

  const { rotaId } = useParams()
  const { fetchRota, rotas } = useRotaService(setIsLoading, setError)

  useEffect(() => {
    const _fetch = async () => rotaId && setRota(await fetchRota(rotaId))

    _fetch()
  }, [rotaId, fetchRota, rotas])

  return (
    <>
      {rota ? (
        <ScheduleProvider rotaId={rota.id} >
          <Scheduler rota={rota} />
        </ScheduleProvider >
      ) : (
        isLoading ? <SpinnerLoader /> : error ? Error : null
      )}
    </>
  )
}

export default RotaPage