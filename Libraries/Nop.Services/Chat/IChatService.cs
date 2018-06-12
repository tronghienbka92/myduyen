using Nop.Core.Domain.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Chat
{
    public partial interface IChatService
    {
        #region agent
        List<Agents> GetallAgents(string AgentsName="");
        Agents GetAgentsById(int AgentsId);
        Agents GetAgentsByCustomerId(int CustomerId);
        /// <summary>
        /// Inserts Agents
        /// </summary>
        /// <param name="HopDong">HopDong</param>



        void InsertAgents(Agents _item);

        /// <summary>
        /// Updates the Agents
        /// </summary>
        /// <param name="HopDong">HopDong</param>
        void UpdateAgents(Agents _item);
        /// <summary>
        /// Delete Agents
        /// </summary>
        /// <param name="HopDong">HopDong</param>
        void DeleteAgents(Agents _item);
        #endregion
        #region Convertation
        List<Convertation> GetallConvertation(int agentsid);
        List<Convertation> GetallConversation_History(int agentsid);
        Convertation GetConvertationById(int ConvertationId);
        Convertation GetConvertationBySessionCustomer(string session);
        /// <summary>
        /// Inserts Convertation
        /// </summary>
        /// <param name="HopDong">HopDong</param>



        void InsertConvertation(Convertation _item);

        /// <summary>
        /// Updates the Convertation
        /// </summary>
        /// <param name="HopDong">HopDong</param>
        void UpdateConvertation(Convertation _item);
       
        #endregion
        #region Messenger
        List<Messenger> GetallMessengerByConvertationId(int ConvertationId = 0);
        List<Messenger> GetMessengerNew(int ConvertationId, int MessengerLastId);
        Messenger GetMessengerById(int MessengerId);

        /// <summary>
        /// Inserts Messenger
        /// </summary>
        /// <param name="HopDong">Messenger</param>



        void InsertMessenger(Messenger _item);
        void UpdateMessenger(Messenger _item);
       
        
        #endregion
    }
}
