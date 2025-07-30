import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Maestro {
  id: string;
  nombre: string;
  apellido: string;
  fechaNacimiento: Date;
  correo: string;
  telefono?: string;
  materia?: string;
}

@Injectable({ providedIn: 'root' })
export class MaestroService {
  private apiUrl = '/api/maestro'; 

  constructor(private http: HttpClient) {}

  getMaestros(pageNumber: number = 1, pageSize: number = 10) {
    return this.http.get<any>(`/api/maestro/pagination?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  registrarMaestro(maestro: any): Observable<any> {
    return this.http.post(this.apiUrl, maestro);
  }

  async obtenerMaestroPorId(id: number): Promise<Maestro> {
    const response = await fetch(`${this.apiUrl}/${id}`);
    if (!response.ok) {
      throw new Error('Error al obtener el maestro');
    }
    return response.json();
  }

  async crearMaestro(maestro: Maestro): Promise<Maestro> {
    const response = await fetch(this.apiUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(maestro),
    });
    if (!response.ok) {
      throw new Error('Error al crear el maestro');
    }
    return response.json();
  }

  actualizarMaestro(id: string, maestro: any) {
    return this.http.put(`/api/maestro/${id}`, maestro);
  }

  borrarMaestro(id: string): Observable<any> {
    return this.http.delete(`/api/maestro/${id}`);
  }
}
