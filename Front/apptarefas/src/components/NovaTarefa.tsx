import { ChangeEvent, useState } from "react"
import { appsettings } from "../settings/appsettings"
import { useNavigate } from "react-router-dom"
import Swal from "sweetalert2"
import { ITarefas } from "../interfaces/ITarefas" 
import { Container,Row,Col, Form,FormGroup, Label, Input, Button } from "reactstrap"

const initialTarefas = {
    id: 0,
    titulotarefa: "",
    descricaotarefa: "",
    datacriacao: "",
    dataconclusao: "",
    status: "EmProgresso"
}

export function NovaTerefa() {

    const [tarefas,settarefas] = useState<ITarefas>(initialTarefas);
    const navigate = useNavigate(); 
    
    const inputChangeValue = (event : ChangeEvent<HTMLInputElement>)=> {
        const inputName = event.target.name;
        const inputValue = event.target.value;    
        settarefas({ ...tarefas, [inputName] : inputValue})
    }

    const salvar = async () =>{
        const response = await fetch(`${appsettings.apiUrl}Tarefas/Nova`,{
             method: 'POST',
             headers:{
                  'Content-Type': 'application/json'
             },
             body: JSON.stringify(tarefas)
        })
        if(response.ok){
             navigate("/")
        }else{
             Swal.fire({
                  title: "Error!",
                  text: "Não foi possível salvar Tarefa",
                  icon: "warning"
                });
        }

   }    

   const retornar = () =>{
    navigate("/")
   }

    return (
        <Container className="mt-5">
               <Row>
                    <Col sm={{size:8, offset:2}}>
                         <h4>Nova Tarefa</h4>
                         <hr/>
                         <Form>
                              <FormGroup>
                                   <Label>Titulo tarefa</Label>
                                   <Input type="text" name="titulotarefa" onChange={inputChangeValue} value={tarefas.titulotarefa} />
                              </FormGroup>
                              <FormGroup>
                                   <Label>Descrição tarefa</Label>
                                   <Input type="text" name="descricaotarefa" onChange={inputChangeValue} value={tarefas.descricaotarefa} />
                              </FormGroup>  
                              <FormGroup>
                                   <Label>Data Criação</Label>
                                   <Input type="date" name="datacriacao" placeholder="dd/mm/yyyy" onChange={inputChangeValue} value={tarefas.datacriacao} />
                              </FormGroup>
                              <FormGroup>
                                   <Label>Data Conclusão</Label>
                                   <Input type="date" name="dataconclusao" placeholder="dd/mm/yyyy" onChange={inputChangeValue} value={tarefas.dataconclusao} />
                              </FormGroup>                                    
                              <FormGroup>
                                   <Label>Status da Tarefa</Label>
                                   <Input 
                                      type="select" 
                                      name="status" 
                                      onChange={inputChangeValue} 
                                      value={tarefas.status}                                    
                                    >
                                        <option>
                                            Pendente
                                        </option>
                                        <option>
                                            EmProgresso
                                        </option>
                                        <option>
                                            Concluída
                                        </option>                                        
                                    </Input>
                               </FormGroup>                                                            
                         </Form>                         
                         <Button color="primary" className="me-4" onClick={salvar}>Salvar</Button>
                         <Button color="secondary"  onClick={retornar}>Retornar</Button>
                    </Col>
               </Row>
         </Container>
    )
        
}