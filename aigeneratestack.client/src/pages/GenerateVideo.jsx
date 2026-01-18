// import { useState } from "react";
// import { videoApi} from "../api/videoApi";
// import {useGenerateVideo} from "../hooks/useGenerateVideo";
// export default function GenerateVideo({ onCreated }) {
//   const [form, setForm] = useState({
//     topic: ""
//   });

//   //const [loading, setLoading] = useState(false);
//   //const [error, setError] = useState(null);
  
//   const {generate, loading, error}= useGenerateVideo();

//   async function onSubmit() {
//     try {
//       //setLoading(true);
//       //setError(null);
     
//       const res = await generate(form);
//       onCreated(res.id);
//     } catch (e) {
//       console.log(e);
//       //setError(e.message);
//     } finally {
     
//       //setLoading(false);
//     }
//   }

//   return (
//     <div className="w-full max-w-xl p-6">
//       <div className="bg-white dark:bg-gray-900 border dark:border-gray-800 rounded-2xl shadow-xl p-8 space-y-6">

//         <h2 className="text-2xl font-bold text-center text-gray-900 dark:text-white">
//           Create AI Video
//         </h2>

//         <p className="text-center text-gray-600 dark:text-gray-400">
//           Enter your script and let AI generate a video with voice & visuals.
//         </p>

//         <div>
//           <label className="block mb-2 text-sm font-medium text-gray-700 dark:text-gray-300">
//             Video Script
//           </label>

//           <textarea
//             value={form.topic}
//             onChange={e => setForm({ ...form, topic: e.target.value })}
//             placeholder="Explain async/await in .NET with a simple example..."
//             className="w-full min-h-[140px] p-4 rounded-xl border dark:border-gray-700 bg-white dark:bg-gray-800 text-gray-900 dark:text-white focus:ring-2 focus:ring-indigo-500"
//           />
//         </div>

//         <button
//           onClick={onSubmit}
//           disabled={!form.topic || loading}
//           className="w-full py-3 rounded-xl font-semibold text-white
//           bg-gradient-to-r from-indigo-600 to-purple-600
//           hover:from-indigo-500 hover:to-purple-500
//           active:scale-[0.98] transition-all disabled:opacity-50"
//         >
//           {loading ? "Generating..." : "🚀 Generate Video"}
//         </button>

//         {error && (
//           <p className="text-center text-sm text-red-500">
//             {error}
//           </p>
//         )}
//       </div>
//     </div>
//   );
// }
import { useState } from "react";
import { useGenerateVideo } from "../hooks/useGenerateVideo";

export default function GenerateVideo({ onCreated }) {
  const [advanced, setAdvanced] = useState(false);

  const [form, setForm] = useState({
    topic: "",
    style: "professional",
    lengthSec: 60,
    voice: "en-US-JennyNeural",
    script: ""
  });

  const { generate, loading, error } = useGenerateVideo();

  const autoScript = (topic) =>
    `Create a ${form.style} narrated educational video explaining: ${topic}`;

  async function onSubmit() {
    const payload = {
      topic: form.topic,
      style: form.style,
      lengthSec: form.lengthSec,
      voice: form.voice,
      script: form.script || autoScript(form.topic)
    };

    const res = await generate(payload);
    onCreated(res.id);
  }

  return (
    <div className="max-w-2xl mx-auto p-6">
      <div className="bg-white dark:bg-gray-900 rounded-2xl shadow-xl p-8 space-y-6">

        <h2 className="text-2xl font-bold text-center">
          🎬 AI Video Generator
        </h2>

        {/* Topic */}
        <textarea
          placeholder="Explain async/await in .NET with a simple example..."
          className="w-full min-h-[120px] p-4 rounded-xl border"
          value={form.topic}
          onChange={(e) => setForm({ ...form, topic: e.target.value })}
        />

        {/* Toggle */}
        <button
          type="button"
          onClick={() => setAdvanced(!advanced)}
          className="text-indigo-600 text-sm underline"
        >
          {advanced ? "Hide Advanced Options" : "Show Advanced Options"}
        </button>

        {/* Advanced Options */}
        {advanced && (
          <div className="space-y-4 border-t pt-4">

            {/* Style */}
            <select
              className="w-full p-3 rounded-xl border"
              value={form.style}
              onChange={(e) => setForm({ ...form, style: e.target.value })}
            >
              <option value="professional">Professional</option>
              <option value="casual">Casual</option>
              <option value="storytelling">Storytelling</option>
              <option value="educational">Educational</option>
            </select>

            {/* Length */}
            <input
              type="number"
              min="30"
              max="300"
              className="w-full p-3 rounded-xl border"
              value={form.lengthSec}
              onChange={(e) =>
                setForm({ ...form, lengthSec: Number(e.target.value) })
              }
              placeholder="Video length (seconds)"
            />

            {/* Voice */}
            <select
              className="w-full p-3 rounded-xl border"
              value={form.voice}
              onChange={(e) => setForm({ ...form, voice: e.target.value })}
            >
              <option value="en-US-JennyNeural">Jenny (US Female)</option>
              <option value="en-US-GuyNeural">Guy (US Male)</option>
              <option value="en-IN-NeerjaNeural">Neerja (Indian Female)</option>
              <option value="en-IN-PrabhatNeural">Prabhat (Indian Male)</option>
            </select>

            {/* Script */}
            <textarea
              placeholder="Optional: override auto-generated script"
              className="w-full min-h-[120px] p-4 rounded-xl border"
              value={form.script}
              onChange={(e) =>
                setForm({ ...form, script: e.target.value })
              }
            />
          </div>
        )}

        {/* Submit */}
        <button
          onClick={onSubmit}
          disabled={!form.topic || loading}
          className="w-full py-3 rounded-xl font-semibold text-white
          bg-gradient-to-r from-indigo-600 to-purple-600
          disabled:opacity-50"
        >
          {loading ? "Generating Video..." : "🚀 Generate Video"}
        </button>

        {error && <p className="text-red-500 text-center">{error}</p>}
      </div>
    </div>
  );
}
