import sys
import asyncio
import edge_tts

async def main():
    text = sys.argv[1]
    voice = sys.argv[2]
    output = sys.argv[3]

    communicate = edge_tts.Communicate(text, voice)
    await communicate.save(output)

asyncio.run(main())
