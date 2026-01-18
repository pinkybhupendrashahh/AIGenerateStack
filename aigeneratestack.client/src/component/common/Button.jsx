export default function Button({ children, loading, ...props }) {
  return (
    <button
      {...props}
      disabled={loading}
      className="w-full py-3 rounded-xl font-semiboldbg-indigo-600 text-white hover:bg-indigo-700 transition disabled:opacity-50" >
      {loading ? "Processing..." : children}
    </button>
  );
}
