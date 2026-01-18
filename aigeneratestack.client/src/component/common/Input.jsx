export default function Input({ label, ...props }) {
  return (
    <div className="space-y-1">
      {label && (
        <label className="text-sm font-medium text-gray-700 dark:text-gray-300">
          {label}
        </label>
      )}
      <textarea
        {...props}
        className="
          w-full p-3 rounded-xl border
          border-gray-300 dark:border-gray-600
          bg-white dark:bg-slate-700
          focus:ring-2 focus:ring-indigo-500
          outline-none resize-none
        "
      />
    </div>
  );
}
