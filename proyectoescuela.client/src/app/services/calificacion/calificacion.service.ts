import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


export interface Calificacion {
  id: string;
  participacion: number;
  primerParcial: number;
  segundoParcial: number;
  examenFinal: number;
  trabajoInvestigacion: number;
  trabajoFinal: number;
  nota: number;
  idAlumno: string;
  idMateria: string;
  fechaRegistro: string;  // <-- esta línea agrega aquí
  fechaModificacion?: string;
}


@Injectable({ providedIn: 'root' })
export class CalificacionService {
  private apiUrl = '/api/calificacion';

  constructor(private http: HttpClient) { }

  // Obtener lista paginada
  getCalificaciones(pageNumber: number = 1, pageSize: number = 10) {
    return this.http.get<any>(`${this.apiUrl}/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  // Registrar calificación (HTTP)
  registrarCalificacion(calificacion: any): Observable<any> {
    return this.http.post(this.apiUrl, calificacion);
  }

  // Obtener por ID (async/fetch)
  async obtenerCalificacionPorId(id: number): Promise<Calificacion> {
    const response = await fetch(`${this.apiUrl}/${id}`);
    if (!response.ok) {
      throw new Error('Error al obtener la calificación');
    }
    return response.json();
  }

  // Crear nueva calificación (async/fetch)
  async crearCalificacion(calificacion: Calificacion): Promise<Calificacion> {
    const response = await fetch(this.apiUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(calificacion),
    });
    if (!response.ok) {
      throw new Error('Error al crear la calificación');
    }
    return response.json();
  }

  actualizarCalificacion(calificacionUpdateDto: any): Observable<any> {
    return this.http.put('/api/calificacion/actualizar', calificacionUpdateDto);
  }


  borrarCalificacion(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
