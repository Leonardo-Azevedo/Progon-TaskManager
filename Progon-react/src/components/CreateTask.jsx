import { useState } from "react";

export default function CreateTask({ onAddTaskSubmit }) {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [type, setType] = useState("");
  const [startDate, setStartDate] = useState("");
  const [status, setStatus] = useState("");

  return (
    <div className="space-y-4 shadow w-[800px] h-[320px] bg-stone-800 rounded-lg p-6 flex flex-col">
      <div className="inline-grid grid-cols-2 gap-4">
        <input
          type="text"
          placeholder="Task name"
          className="border border-slate-300 outline-black px-4 py-4 rounded-md"
          value={name}
          onChange={(event) => setName(event.target.value)}
        />
        <input
          type="number"
          placeholder="Type task"
          max={2}
          min={0}
          className="border border-slate-300 outline-black px-4 py-4 rounded-md"
          value={type}
          onChange={(event) => setType(event.target.value)}
        />
        <input
          type="text"
          placeholder="Task description"
          className="col-span-2 border border-slate-300 outline-black px-4 py-4 rounded-md"
          value={description}
          onChange={(event) => setDescription(event.target.value)}
        />
        <input
          type="date"
          placeholder="Start date"
          className="border border-slate-300 outline-black px-4 py-4 rounded-md"
          value={startDate}
          onChange={(event) => setStartDate(event.target.value)}
        />
        <input
          type="number"
          placeholder="Status"
          max={1}
          min={0}
          className="border border-slate-300 outline-black px-4 py-4 rounded-md"
          value={status}
          onChange={(event) => setStatus(event.target.value)}
        />
      </div>

      <button
        onClick={() =>
          onAddTaskSubmit(name, description, type, startDate, status)
        }
        className="text-lg text-white bg-gradient-to-r from-purple-600 to-purple-900 px-4 py-2 rounded-md"
      >
        Create
      </button>
    </div>
  );
}
