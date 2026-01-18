import { useState } from "react";
import { videoApi } from "../api/videoApi";

export function useGenerateVideo() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const generate = async (payload) => {
    try {
      setLoading(true);
      setError(null);
      return await videoApi.generate(payload);
    } catch (e) {
      setError(e.message || "Video generation failed");
      throw e;
    } finally {
      setLoading(false);
    }
  };

  return { generate, loading, error };
}
