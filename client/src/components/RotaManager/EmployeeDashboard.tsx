import { useState } from "react";
import { useParams } from "react-router";
import { Button, Toolbar } from "../Common";
import { AddEmployeeModal, EmployeeList } from ".";

const EmployeeDashboard = () => {
  const { isAddEmployeeOpen } = useParams();

  const [addEmployeeOpen, setAddEmployeeOpen] = useState(isAddEmployeeOpen ? true : false);

  return (
    <>
      <>
        <Toolbar title="Employees">
          <Button buttonType="Toolbar" content="Add employee" action={() => setAddEmployeeOpen(true)} />
        </Toolbar>
        <EmployeeList setAddEmployeesOpen={setAddEmployeeOpen} />
      </>
      <>
        <AddEmployeeModal isOpen={addEmployeeOpen} close={() => setAddEmployeeOpen(false)} />
      </>
    </>
  )
}

export default EmployeeDashboard;