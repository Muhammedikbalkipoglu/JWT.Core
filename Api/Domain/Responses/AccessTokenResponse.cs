﻿using Api.Security.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Responses
{
    public class AccessTokenResponse : BaseResponse
    {
        public AccessToken accessToken { get; set; }
        private AccessTokenResponse(bool success, string message, AccessToken accessToken)
            : base(success, message)
        {
            this.accessToken = accessToken;
        }

        //success
        public AccessTokenResponse(AccessToken accessToken) : this
            (true, string.Empty, accessToken)
        { 
        
        }

        //fail
        public AccessTokenResponse(string message) : this(false, message, null) 
        { 
        }
    }
}
