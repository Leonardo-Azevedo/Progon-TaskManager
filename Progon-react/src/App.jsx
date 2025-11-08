import { useEffect, useState } from "react";
import Tasks from "./components/Tasks";
import CreateTask from "./components/CreateTask";
import api from "../../Progon-react/src/services/api";

export default function App() {
  const [allTasks, setTasks] = useState({
    pending: [],
    inProgress: [],
    done: [],
  });

  // Requisição da API para listagem total
  async function getAllTask() {
    const tasksFromAPI = await api.get("/list");
    setTasks({
      pending: tasksFromAPI.data.filter((t) => t.status === 0),
      inProgress: tasksFromAPI.data.filter((t) => t.status === 1),
      done: tasksFromAPI.data.filter((t) => t.status === 2),
    });
  }

  // Carregar ao iniciar a tela
  useEffect(() => {
    getAllTask();
  }, []);

  async function onAddTaskSubmit(name, description, type, startDate) {
    await api.post("/createTask", {
      Name: name,
      Description: description,
      Type: type,
      StartDate: startDate,
    });

    getAllTask();
  }

  async function onDeleteTask(id) {
    await api.delete(`/deleteTask?id=${id}`);

    getAllTask();
  }

  return (
    <div className="w-screen h-screen bg-stone-950 ">
      <div className="p-6 space-y-4">
        <div className="justify-center flex">
          <CreateTask onAddTaskSubmit={onAddTaskSubmit} />
        </div>
        <div className="justify-center flex gap-4">
          {/* Chamada do componente Tasks */}
          <Tasks
            tas={allTasks.pending}
            title="Pending"
            onDeleteTask={onDeleteTask}
          />
          <Tasks
            tas={allTasks.inProgress}
            title="In progress"
            onDeleteTask={onDeleteTask}
          />
          <Tasks tas={allTasks.done} title="Done" onDeleteTask={onDeleteTask} />
        </div>
      </div>
    </div>
  );
}
