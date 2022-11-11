from fastapi import FastAPI
from routers import tts

import sys

print(sys.executable)
app = FastAPI()

app.include_router(tts.router)


@app.get("/")
async def root():
    return {"message": "Hello AiRang !"}
