
using System.Collections.Generic;

using ACR.Assist.AuditTrail.Core.DTO;

namespace ACR.Assist.AuditTrail.Service.Tests
{
   public class UnitTestData
    {

        /// <summary>
        /// Gets the add audit trail entries.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetAddAuditTrailEntries()
        {
            yield return new object[] {
                    new List<AddAuditTrailEntry>(){
                      new AddAuditTrailEntry() {
                          Username =string.Empty,
                          ModuleName =string.Empty,
                          ActionType =string.Empty,
                          Description =string.Empty,
                          Detailed_Description =string.Empty
                      },
                        new AddAuditTrailEntry() {
                          Username ="Test",
                          ModuleName ="LI-RADS",
                          ActionType ="Add",
                          Description ="Unit Test",
                          Detailed_Description ="Detailed Unit Test"
                      }
                    }
                };
        }
    }
}
