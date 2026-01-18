import Button from "../components/Button";
import StatusBadge from "../components/StatusBadge";

export default function VideoPreview({
  videoUrl,
  status,
  onRetry,
}) {
  if (!videoUrl && status !== "Completed") return null;

  return (
    <div className="mt-8 space-y-4">
      <div className="flex items-center justify-between">
        <h3 className="text-lg font-semibold">Generated Video</h3>
        <StatusBadge status={status} />
      </div>

      <div className="rounded-2xl overflow-hidden shadow-lg bg-black">
        {videoUrl ? (
          <video
            src={videoUrl}
            controls
            className="w-full max-h-[420px]"
          />
        ) : (
          <div className="p-6 text-center text-white">
            Video not available
          </div>
        )}
      </div>

      <div className="flex gap-4">
        <a
          href={videoUrl}
          download
          className="flex-1"
        >
          <Button>Download</Button>
        </a>

        <Button onClick={onRetry} className="flex-1">
          Regenerate
        </Button>
      </div>
    </div>
  );
}
