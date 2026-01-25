
import { STATUS_PROGRESS } from "../utils/progressMap";
import AnimatedProgress from "../component/common/AnimatedProgress";
import StatusBadge from "../component/common/StatusBadge";
import Button from "../component/common/Button";
import { videoApi } from "../api/videoApi";

import  {useVideoStatus}  from "../hooks/useVideoStatus";
export  function VideoStatusPage({ jobId }) {
    const { job, error } = useVideoStatus(jobId);
console.log("VideoStatusPage mounted with jobId:", jobId);
  if (!job) return <p className="p-6">⏳ Initializing job...</p>;
  if (error) return <p className="p-6 text-red-500">{error}</p>;

  const progress = STATUS_PROGRESS[job.status] ?? job.progress ?? 0;

  return (
    <div className="max-w-lg mx-auto p-5 space-y-4">
      <h2 className="text-xl font-bold">🎬 Generating Your AI Video</h2>

      <StatusBadge status={job.status} />
      <AnimatedProgress value={progress} />

      {/* FINAL VIDEO */}
      {job.status === "Completed" && (
        <div className="mt-6 space-y-3">
          <video
            src={job.videoUrl}
            controls
            className="rounded-xl shadow-lg w-full"
          />
          <a
            href={job.videPath}
            download
            className="block text-center underline text-sm"
          >
            Download Video
          </a>
        </div>
      )}

      {/* FAILURE */}
      {job.status === "Failed" && (
        <div className="space-y-3">
                  <p className="text-red-500 text-sm">{job.error}</p>
                  <Button onClick={() => videoApi.retry(jobId)}>
            Retry
          </Button>
        </div>
      )}

      {/* CANCEL */}
      {["Queued", "ScriptGenerated", "AudioGenerated"].includes(job.status) && (
        <Button
          variant="danger"
          onClick={() => videoApi.cancel(jobId)}
        >
          Cancel Job
        </Button>
      )}
    </div>
  );
}
