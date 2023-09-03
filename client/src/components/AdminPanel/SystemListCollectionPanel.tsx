import { useState } from "react"
import { FieldArray, FieldArrayRenderProps, Form, Formik } from "formik"
import { PlusIcon } from "@heroicons/react/solid"
import { useFetch, useRequestBuilderService, useListCollectionService } from "../../hooks"
import { IFetch, IList, IListCollection, IListValue } from "../../models"
import { generateColour } from "../../services"
import { endpoints } from "../../config"
import { Button, Dialog, Fetch, FetchError, SpinnerIcon } from "../Common"
import { EditableSubPanelTitle, ListItem, Panel, SubPanel, SubPanelContent } from "."

const SystemListCollectionPanel = () => {

  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [error, setError] = useState<any>()
  const { buildRequest } = useRequestBuilderService()
  const { init, update } = useListCollectionService()

  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false)

  const handleDelete = async (arrayHelpers: FieldArrayRenderProps, index: number) => {
    arrayHelpers.remove(index)
    setDeleteDialogOpen(false)
  }

  return (
    <Fetch
      fetchOutput={useFetch(endpoints.systemlistcollections, buildRequest(), [], setIsLoading, setError)}
      render={({ response }: IFetch<IListCollection>) => (
        <>
          {!isLoading && response && response.lists ? (
            <Formik
              initialValues={response}
              onSubmit={(values) => {
                update(response._id, values)
              }}
            >
              {({ values, setFieldValue }) => (
                <Form>
                  <FieldArray
                    name="lists"
                    render={arrayHelpers => (
                      <Panel title="System List Collection" subtitle={`Last updated ${new Date(response.updatedAt || '').toLocaleDateString()}`}
                        HeaderActions={
                          <Button
                            content="Add list"
                            type="button"
                            Icon={PlusIcon}
                            action={() => arrayHelpers.push({ name: '', description: '', values: [] })}
                          />
                        }
                      >
                        <div className="flex flex-col space-y-4">
                          {values.lists.map((list: IList, index: number) => (
                            <SubPanel
                              key={index}
                              Title={<EditableSubPanelTitle name={`lists.${index}.description`} placeholder="Descriptive name" />}
                              Subtitle={<EditableSubPanelTitle name={`lists.${index}.name`} placeholder="Internal field name" subtitle />}
                              HeaderActions={
                                <>
                                  <Button type="button" buttonType="Cancel" content="Delete" action={() => setDeleteDialogOpen(true)} />
                                  <Dialog
                                    isOpen={deleteDialogOpen}
                                    close={() => setDeleteDialogOpen(false)}
                                    positiveAction={() => handleDelete(arrayHelpers, index)}
                                    title="Delete system list"
                                    description="This action will delete the system list"
                                    content="If you choose to continue the system list will be deleted - this could cause application breaking problems."
                                  />
                                </>
                              }
                            >
                              <FieldArray
                                name={`lists.${index}.values`}
                                render={arrayHelpers => (
                                  <SubPanelContent
                                    SubPanelActions={
                                      <div className="flex justify-end">
                                        <Button content="Add item" type="button" action={() => arrayHelpers.push({ short: '', long: '', colour: generateColour() })} />
                                      </div>
                                    }
                                  >
                                    {list.values.map((value: IListValue, vindex: number) => (
                                      <ListItem
                                        key={vindex}
                                        name={`lists.${index}.values.${vindex}`}
                                        setFieldValue={setFieldValue}
                                        remove={() => arrayHelpers.remove(vindex)}
                                        index={vindex}
                                        colour={value.colour}
                                      />
                                    ))}
                                  </SubPanelContent>
                                )}
                              />
                            </SubPanel>
                          ))}
                        </div>
                      </Panel>
                    )}
                  />
                </Form>
              )}
            </Formik>
          ) : (
            <Panel title="System List Collection" hideSave>
              {isLoading ? (
                <SpinnerIcon className="w-6 h-6" />
              ) : (
                error ? (
                  <FetchError error={error} isLoading={isLoading} toggleRefresh={() => {}} />
                ) : (
                  <div className="flex justify-center p-10">
                    <button className="font-bold text-xl tracking-wider hover:text-gray-600 transition-colors" type="button" onClick={() => init()}>System list collection has not been created yet, click here to create it</button>
                  </div>
                )
              )}
            </Panel>
          )}
        </>
      )
      }
    />
  )
}

export default SystemListCollectionPanel