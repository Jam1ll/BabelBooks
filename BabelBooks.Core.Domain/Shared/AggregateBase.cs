namespace BabelBooks.Core.Domain.Shared
{
    public abstract class AggregateBase
    {
        public Guid Id { get; protected set; }

        //eventos que han ocurrido pero no han sido guardados
        private readonly List<object> _uncommitedEvents = [];

        //metodo para obtener los eventos no guardados
        public object[] GetUncommitedEvents() => [.. _uncommitedEvents]; // -> _uncommitedEvents.ToArray();

        //metodo para limpiar la lista de eventos luego de guardar
        public void ClearUncommitedEvents()=> _uncommitedEvents.Clear();

        //metodo para aplicar un evento al estado del agregado actual
        protected void ApplyEvent(object @event)
        {
            //llamar al metodo ApplyEvent especifico para el tipo de evento mediante reflection
            var method = GetType().GetMethod("ApplyEvent", [@event.GetType()])
                //si no se encuentra el metodo...
                ?? throw new InvalidOperationException($"No se encontro el metodo ApplyEvent para el evento de tipo {@event.GetType().Name}");
            method.Invoke(this, [@event]);
        }
        
        //metodo para que las clases derivadas registren un nuevo evento
        public void RaiseEvent(object @event)
        {
            ApplyEvent(@event); //aplicar evento al estado actual del agregado
            _uncommitedEvents.Add(@event); //agregar evento a la lista de eventos no guardados
        }
    }
}
