import { BrowserRouter, Route, Routes } from "react-router-dom"
import { Lista } from "./components/Lista"
import { NovaTerefa } from "./components/NovaTarefa"
import { EditarTerefa } from "./components/EditarTarefa"

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Lista/>} /> 
        <Route path="/Tarefas/Nova" element={<NovaTerefa/>} />
        <Route path="/Tarefas/Editar/:id" element={<EditarTerefa/>} /> 
      </Routes>
    </BrowserRouter>  
  )
}

export default App
