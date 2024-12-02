import { useEffect, useState } from "react"
import { appsettings } from "../settings/appsettings"
import { Link } from "react-router-dom"
import Swal from "sweetalert2"
import { ITarefas } from "../interfaces/ITarefas" 
import { Container, Row, Col, Table, Button, Input, Label} from "reactstrap"

export function Lista() {

    const [tarefas, setTarefas] = useState<ITarefas[]>([]); 
    const [search, setSearch] = useState("");
    const [tipocons, setTipocons] = useState("");

    const buscarTarefas = async () => {
         const response = await fetch(`${appsettings.apiUrl}Tarefas/Lista`)
         if (response.ok) {
              const data = await response.json();
              setTarefas(data)
         }
    } 
    
    const buscaTarefasByValor = async (byValor: string) => {
         if (byValor != "")  {
            if (tipocons == "1") { //consulta por id
               const response = await fetch(`${appsettings.apiUrl}Tarefas/Busca/${byValor}`)
          
               if (response.ok) {
                    const data = await response.json();
                    setTarefas(data)
               } 
          } else 
            if (tipocons == "2") { //consulta por descrição da tarefa
               const response = await fetch(`${appsettings.apiUrl}Tarefas/descricaoTarefa/${byValor}`)
          
               if (response.ok) {
                    const data = await response.json();
                    setTarefas(data)
               }
            } else
               if (tipocons == "3") { //Consulta pelo status
                    const response = await fetch(`${appsettings.apiUrl}Tarefas/statusTarefa/${byValor}`)
               
                    if (response.ok) {
                         const data = await response.json();
                         setTarefas(data)
                    }
              }
          } else {
               buscarTarefas();   
          }          
    } 

    const handleChange = (e: { target: { value: any } }) => {
          setTipocons(e.target.value);
          console.log(e.target.value);
    }
    
    useEffect(() => {              
   }, [search]) 

    useEffect(() => {
        buscarTarefas()
   }, []) 
   
   const Excluir = (id: number) => {
    Swal.fire({
         title: "Você tem certeza?",
         text: "Excluir Tarefas!",
         icon: "warning",
         showCancelButton: true,
         confirmButtonColor: "#3085d6",
         cancelButtonColor: "#d33",
         confirmButtonText: "Sim, excluir!"
    }).then(async (result) => {
         if (result.isConfirmed) {

              const response = await fetch(`${appsettings.apiUrl}Tarefas/Excluir/${id}`, { method: "DELETE" })
              if (response.ok) await buscarTarefas()
         }
    });
  }
  
    return (
        <Container className="mt-5">
               <Row>
                    <Col sm={{ size: 30, offset: -2 }}>
                         <h4>Lista de Tarefas</h4>
                         <hr />
                         <Link className="btn btn-success mb-3" to="/Tarefas/Nova" >Nova Tarefa</Link>                         
                         <hr /> 
                        
                        <form>                         
                              <Input name="radio" type="radio" id="idterafa" checked={tipocons === "1"} value={"1"}  onChange={handleChange} />
                                   {' '}
                                   <Label check>
                                   Consulta por id
                                   </Label><br />                                   
                                                                      
                              <Input name="radio" type="radio" id="descricaoterafa" checked={tipocons === "2"} value={"2"} onChange={handleChange} />
                                   {' '}
                                   <Label check>
                                   Consulta por Descrição da Tarefa Exemplo : %descricao% ou %descricao ou descricao%
                                   </Label><br />                                                          

                              <Input name="radio" type="radio" id="statusterafa" checked={tipocons === "3"} value={"3"} onChange={handleChange} />
                                   {' '}
                                   <Label check>
                                   Consulta por Status da Tarefa
                                   </Label>                                    
                         </form>                      

                         Opção selecionada foi <strong>{tipocons}</strong>

                         <Input type='search' placeholder="Digite o valor que deseja consultar..." onChange={(e) => setSearch(e.target.value)} />

                         <hr />                                 
                         <Button color="primary" onClick={() => { buscaTarefasByValor(search) }}>
                              Consultar
                         </Button>                                                                                                                                                                            
                         <hr />                     
                         <Table striped>
                              <thead>
                                   <tr>
                                        <th>Titulo da Tarefa</th>
                                        <th>Descrição da Tarefa</th>
                                        <th>Dt.Criação</th>
                                        <th>Dt.Conclusão</th>
                                        <th>Status</th>
                                        <th></th>
                                        <th></th>
                                   </tr>
                                   <br />
                              </thead>
                              <tbody>
                                   {
                                        tarefas.map((item) => (
                                             <tr key={item.id}>
                                                  <td>{item.titulotarefa}</td>
                                                  <td>{item.descricaotarefa}</td>
                                                  <td>{item.datacriacao}</td>
                                                  <td>{item.dataconclusao}</td>
                                                  <td>{item.status}</td>
                                                  <td>
                                                    <Link className="btn btn-primary me-0" to={`/Tarefas/Editar/${item.id}`} >Editar</Link>
                                                  </td>
                                                  <td>
                                                    <Button color="danger" onClick={() => { Excluir(item.id!) }}>
                                                            Excluir
                                                    </Button>                                                    
                                                  </td>
                                             </tr>
                                        ))
                                   }
                              </tbody>
                              
                         </Table>
                    </Col>
               </Row>
        </Container>
    )

      
}