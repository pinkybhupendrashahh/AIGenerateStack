import { useEffect, useState } from "react";
import { videoApi } from "../api/videoApi";

const TERMINAL_STATES = ["Completed", "Failed", "Cancelled"];

export function useVideoStatus(jobId) {
  const [job, setJob] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (!jobId) return;

    let cancelled = false;
    let timer;

    const poll = async () => {
      try {
        const res = await videoApi.status(jobId);
        if (cancelled) return;

        setJob(res);

        if (!TERMINAL_STATES.includes(res.status)) {
          timer = setTimeout(poll, 3000);
        }
      } catch (err) {
        if (!cancelled) setError(err.message);
      }
    };

    setError(null);
    poll();

    return () => {
      cancelled = true;
      clearTimeout(timer);
    };
  }, [jobId]);

  return {
    status: job?.status,
    progress: job?.progress ?? 0,
    videoUrl: job?.videoUrl,
    error
  };
}
