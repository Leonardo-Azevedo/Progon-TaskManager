import { TrashIcon } from "lucide-react";

export default function Tasks({ tas, title, onDeleteTask }) {
  return (
    <div className=" shadow relative w-64 h-[400px] bg-stone-800 rounded-lg overflow-hidden">
      <div className=" h-[55px] bg-gradient-to-r from-purple-600 to-purple-900 rounded-lg shadow-[0_3px_15px_rgba(0,0,0,0.25)] flex items-center justify-center">
        <h2 className="p-1 text-lg font-bold text-white">{title}</h2>
      </div>

      <ul className="space-y-4 p-3">
        {tas.map((t) => (
          <li key={t.id} className="flex gap-2">
            <span className="text-white bg-stone-700 rounded-md p-2 w-full">
              {t.name}
            </span>
            <button
              onClick={() => onDeleteTask(t.id)}
              className="text-white bg-stone-700 rounded-md p-2"
            >
              <TrashIcon />
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
