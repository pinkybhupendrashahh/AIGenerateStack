import AnimatedProgress from "../components/AnimatedProgress";
import StatusBadge from "../components/StatusBadge";

export default function VideoStatusCard({ status, progress }) {
  if (!status) return null;

  return (
    <div className="
      mt-6 p-5 rounded-2xl
      bg-white dark:bg-slate-800
      shadow-lg
      transition-all
    ">
      <div className="flex items-center justify-between mb-3">
        <p className="text-sm font-semibold text-gray-700 dark:text-gray-300">
          Video Status
        </p>
        <StatusBadge status={status} />
      </div>

      {progress !== undefined && (
        <AnimatedProgress value={progress} />
      )}
    </div>
  );
}
